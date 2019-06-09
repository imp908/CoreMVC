
import { HubConnectionBuilder } from "@aspnet/signalr";

class SignalRwork{
    constructor(){
        this.connection = new HubConnectionBuilder()
            .withUrl("/rWork").build();
    }
    Init = () => {
        this.connection.on("WorkStatus",(payload)=>{
            console.log(payload);
        });

        //Disable send button until connection is established
        document.getElementById("sendButton").style.disabled = true;
        
        //enable button when connection with hub established
        this.connection.start().then(() => {
            document.getElementById("sendButton").style.disabled = false;
        }).catch((err) => {
            return console.error(err.toString());
        });

        //send message
        document.getElementById("sendButton").addEventListener("click", (event) => {
            this.StartWork();
            event.preventDefault();
        });  
    
    }

    StartWork = () => {
        this.connection.invoke("StartWork")
            .catch((err) => {
                return console.error(err.toString());
            });
    }
  
}

export { SignalRwork}