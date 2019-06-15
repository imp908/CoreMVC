
import { HubConnectionBuilder } from "@aspnet/signalr";

class SignalRwork{
    constructor(){
        this.connection = new HubConnectionBuilder()
            .withUrl("/rWork").build();
    }

    //payload:{eventDate:,status:}
    Init = () => {
        this.connection.on("WorkStatus",(payload)=>{
            console.log(payload);
            this.AddDOMreport(payload);
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
    
    AddDOMreport = (payload) => {
        var item = document.getElementById("messageText");        
        if (!item || !payload || !payload.eventDate || !payload.status){return;}

        const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric' };
        let date = new Date(payload.eventDate);
        const msg = `Date: ${date.toLocaleDateString('en-US', options)}; Status:${payload.status};`;;

        item.innerHTML = msg;

        var list = document.getElementById("messagesList");
        var li = document.createElement("li");
        li.textContent = msg;
        document.getElementById("messagesList").appendChild(li);
    }
  
}

export { SignalRwork}