import React from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faCircleCheck, faCircleXmark} from '@fortawesome/free-regular-svg-icons';
import css from "./NotificationWindow.module.css"

function NotificationWindow ({children ,isSuccess, visible, setVisible}){

  let classes = [css.notificationBase]

  if(!visible){
    classes.push(css.notificationHidden)
  }

  return(
		<div className={classes.join(' ')}>
      <div className={css.notificationBox}>
        {
          isSuccess
          ? <div className={css.notificationText}>
            <FontAwesomeIcon icon={faCircleCheck} color="green" size="4x"/>
            {children}
          </div>
          : <div className={css.notificationText}>
            <FontAwesomeIcon icon={faCircleXmark} color="red" size="4x"/>
            {children}
          </div>
        }
        <button className={css.notificationButton} onClick={() => setVisible(false)}>  
          OK
        </button>
      </div>
		</div>
  );
}

export default NotificationWindow;