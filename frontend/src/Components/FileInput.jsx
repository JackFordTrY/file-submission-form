import React, {useState} from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faFileArrowUp} from '@fortawesome/free-solid-svg-icons';

function FileInput ({onChange, selectedFile}){

  const [isFileSelected, setIsFileSelected]  = useState(false);

  function fileSelectionHandler(event){
		onChange(event.target.files[0]);
		setIsFileSelected(true);
  }

  function cancelSelectionHandler(){
    onChange(undefined)
    setIsFileSelected(false);
  }

  return(
    <div className="form-file-input-container">
      { isFileSelected
        ?(
          <div className="file-input-container">
            <span className="file-input-text">{selectedFile.name}</span>
            <button className="file-input-cancel-button" onClick={cancelSelectionHandler}>Cancel</button>
          </div>
        )
        : (
          <div className="file-input-container">
            <input type="file" className="file-input-hidden" id="file-input" onChange={fileSelectionHandler} accept=".docx"/>
            <label className="file-input-text" htmlFor="file-input">
              <FontAwesomeIcon icon={faFileArrowUp} size="2x"/>
              <p>Drag & Drop to Upload File</p>
              <p>.docx</p>
            </label>
          </div>
        )
      }
    </div>
  )
}

export default FileInput;