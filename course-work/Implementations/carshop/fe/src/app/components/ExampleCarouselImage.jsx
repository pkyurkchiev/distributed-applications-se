
import React from "react";

const ExampleCarouselImage = ({ src, alt }) => {
  return (
    <img
      className="d-block w-100 carousel-img-fixed"
      src={src}
      alt={alt}
      style={{ height: '500px', objectFit: 'cover' }}
    />
  );
};

export default ExampleCarouselImage;
