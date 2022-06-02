'use strict';

const functions = require('firebase-functions');
const mkdirp = require('mkdirp');
const admin = require('firebase-admin');
admin.initializeApp();
const path = require('path');
const os = require('os');
const sharp = require('sharp');

// Max height and width of the thumbnail in pixels.
const THUMB_MAX_HEIGHT = 200;
const THUMB_MAX_WIDTH = 200;
// Thumbnail prefix added to file names.
const THUMB_PREFIX = 'thumb_';

/**
 * When an image is uploaded in the Storage bucket We generate a thumbnail automatically using
 * Sharp.
 * After the thumbnail has been generated and uploaded to Cloud Storage,
 * we write the public URL to the Firebase Realtime Database.
 */
exports.generateThumbnail = functions.storage.object().onFinalize(async (object) => {
  // File and directory paths.
  const filePath = object.name;
  const contentType = object.contentType; // This is the image MIME type
  const fileDir = path.dirname(filePath);
  const fileName = path.basename(filePath);
  const thumbFilePath = path.normalize(path.join(fileDir, `${THUMB_PREFIX}${fileName}`));
  const tempLocalFile = path.join(os.tmpdir(), filePath);
  const tempLocalDir = path.dirname(tempLocalFile);
  const tempLocalThumbFile = path.join(os.tmpdir(), thumbFilePath);

  // Exit if this is triggered on a file that is not an image.
  if (!contentType.startsWith('image/')) {
    return functions.logger.log('This is not an image.');
  }

  // Exit if the image is already a thumbnail.
  if (fileName.startsWith(THUMB_PREFIX)) {
    return functions.logger.log('Already a Thumbnail.');
  }

  // Cloud Storage files.
  const bucket = admin.storage().bucket(object.bucket);
  const file = bucket.file(filePath);
  //const thumbFile = bucket.file(thumbFilePath);
  const metadata = {
    contentType: contentType
  };

  // Create the temp directory where the storage file will be downloaded.
  await mkdirp(tempLocalDir)
  // Download file from bucket.
  await file.download({ destination: tempLocalFile });
  functions.logger.log('The file has been downloaded to', tempLocalFile);
  // Generate a thumbnail using Sharp.
  sharp(tempLocalFile).resize(THUMB_MAX_WIDTH, THUMB_MAX_HEIGHT).toFile(tempLocalThumbFile, (err) => {
    if (err) {
      functions.logger.log(err);
    } else {
      functions.logger.log('Thumbnail created at', tempLocalThumbFile);
    }
  });
  // Uploading the Thumbnail.
  await bucket.upload(tempLocalThumbFile, { destination: thumbFilePath, metadata: metadata, predefinedAcl: 'publicRead' }).then(result => {
    functions.logger.log('Thumbnail uploaded to Storage at', thumbFilePath);
    functions.logger.log('Got Signed URLs.');
    const file = result[0];
    return file.getMetadata();
  }).then(results => {
    const metadata = results[0];
    functions.logger.log('metadata=', metadata.mediaLink);
    // Add the URLs to the Database
    admin.database().ref('images').push({ name: fileName, thumbnail: metadata.mediaLink });
    return functions.logger.log('Thumbnail URLs saved to database.');
  }).catch(error => {
    functions.logger.log(error);
  });
});