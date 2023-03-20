import React, {useState} from "react";
import "./App.css";
import FileInput from "./Components/FileInput";
import NotificationWindow from "./Components/NotificationWindow/NotificationWindow";

function App() {

  const [selectedFile, setSelectedFile] = useState();
  const [isNotificationVisible, setIsNotificationVisible] = useState(false);
  const [isNotificationSuccessful, setIsNotificationSuccessful] = useState(false);
  const [notificationMessage, setNotificationMessage] = useState();

  function fileSelectionHandler(file){
    setSelectedFile(file);
  }

  function displayNotification(text, hasSucceded){
    setIsNotificationVisible(true);
    setIsNotificationSuccessful(hasSucceded);
    setNotificationMessage(text);
  }

  function submintFileHandler(){
    
    const emailInput = document.getElementById("email-input").value;

    const formData = new FormData()
    formData.append("email", emailInput);
    formData.append("file", selectedFile);

    if(selectedFile && emailInput){
      fetch("https://testtaskbackend.azurewebsites.net/api/Home/SubmitFile", {
          method: "POST",
          body: formData,
      })
      .then(response => {
        const json = response.json();

        if(!response.ok) {
          json.then(text => displayNotification("Oops, something went wrong!\n" + text.join('\n'), false));
        }
        else{
          json.then(text => displayNotification(text,true));
        }
      })
    }
    else{
      displayNotification("Oops, something went wrong!\nPlease fill email and file fields!", false);
    }
  }

  return (
    <div className="container">
        <div className="form-wrap">
          <NotificationWindow isSuccess={isNotificationSuccessful} visible={isNotificationVisible} setVisible={setIsNotificationVisible}>
            <p>{notificationMessage}</p>
          </NotificationWindow> 

          <label className="email-text-input-label" htmlFor="email-input">
            Enter your email
            <input type="email" className="form-text-input" id="email-input" placeholder="example123@example.com" />
          </label>
         
          
          <FileInput onChange={fileSelectionHandler} selectedFile={selectedFile}/>

          <button type="button" className="form-submit" onClick={submintFileHandler}>Submit</button>

        </div>
    </div>
  );
}

export default App;
