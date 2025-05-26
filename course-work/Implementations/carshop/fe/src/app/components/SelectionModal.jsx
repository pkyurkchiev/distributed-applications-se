import React from 'react';
import PropTypes from 'prop-types';

const SelectionModal = ({ title, records, onSelect, onClose, renderItem }) => {
  return (
    <div className="modal d-block" tabIndex="-1" style={overlayStyle}>
      <div className="modal-dialog modal-lg modal-dialog-centered">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">{title}</h5>
            <button type="button" className="btn-close" onClick={onClose} />
          </div>
          <div className="modal-body" style={{ maxHeight: '60vh', overflowY: 'auto' }}>
            {records.length === 0 ? (
              <p>No records found.</p>
            ) : (
              <ul className="list-group">
                {records.map((record) => (
                  <li
                    key={record.id}
                    className="list-group-item list-group-item-action"
                    onClick={() => onSelect(record)}
                    style={{ cursor: 'pointer' }}
                    tabIndex={0}
                    onKeyDown={e => {
                      if (e.key === 'Enter' || e.key === ' ') {
                        onSelect(record);
                      }
                    }}
                  >
                    {renderItem ? renderItem(record) : Object.values(record).join(' | ')}
                  </li>
                ))}
              </ul>
            )}
          </div>
          <div className="modal-footer">
            <button className="btn btn-secondary" onClick={onClose}>Close</button>
          </div>
        </div>
      </div>
    </div>
  );
};

const overlayStyle = {
  backgroundColor: 'rgba(0,0,0,0.5)',
  position: 'fixed',
  top: 0, left: 0, right: 0, bottom: 0,
  zIndex: 1050
};

SelectionModal.propTypes = {
  title: PropTypes.string.isRequired,
  records: PropTypes.array.isRequired,
  onSelect: PropTypes.func.isRequired,
  onClose: PropTypes.func.isRequired,
  renderItem: PropTypes.func
};

export default SelectionModal;
