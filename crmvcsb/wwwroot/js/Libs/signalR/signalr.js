
import { HubConnectionBuilder } from "@aspnet/signalr";


class SignalRhub
{
    constructor(){
        this.connection = new HubConnectionBuilder()
            .withUrl("/rHub").build();
    }

    Init = () =>
    {
        //Disable send button until connection is established
        document.getElementById("sendButton").style.disabled = true;

        this.connection.on("ReceiveMessage",  (user, message) => {
            var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
            var encodedMsg = user + " says " + msg;
            var li = document.createElement("li");
            li.textContent = encodedMsg;
            document.getElementById("messagesList").appendChild(li);
        });

        //enable button when connection with hub established
        this.connection.start().then( () => {
            document.getElementById("sendButton").style.disabled = false;
        }).catch( (err) => {
            return console.error(err.toString());
        });

        //send message
        document.getElementById("sendButton").addEventListener("click", (event) => {
            this.SendMessages();
            event.preventDefault();
        });     

    }


    SendMessages = () =>{
        
        this.FetchURL({ url:"/Identity/CustomAccount/GetCurrentUser",method:"GET",params:null})
        .then(r=>{
            if (r && r.userName){
                var message = document.getElementById("messageInput").value;
                this.connection.invoke("SendMessage", r.userName, message)
                    .catch((err) => {
                        return console.error(err.toString());
                    });
            }
        })

    }

    //option:{ url, method, params}
    FetchURL = ( option ) => {

        let _url;

        _url = (option.params) ? `${option.url}?${option.params}`
            : `${option.url}`;

        let _request = new Request(_url,
            { method: option.method, credentials: 'include', mode: 'no-cors' });

        return fetch(_request)
            .then((response)=>{
                return response.json()
            })
            .then((json) => {
                return json;
            });

    }


}

export {SignalRhub}