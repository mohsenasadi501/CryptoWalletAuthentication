import React from "react";
import "./App.css";
import axios from "axios";

function App() {
  const baseURL = "https://localhost:7061/User/nonce";
  const [nonce, setNonce] = React.useState("");

  function getNonnce() {
    axios
      .get(baseURL, {
        params: { publicAddress: "0x174A639d18a2EE6590ed1A201F8CCC76A52FFB13" },
      })
      .then((response) => {
        console.log(response)
        setNonce(response.data);
      });
  }

  return (
    <div className="container">
      <div className="row">
        <div className="col">
          <br></br>
          <button type="button" className="btn btn-primary" onClick={getNonnce}>
            Get Nonce
          </button>
          <input type="text" value={nonce}></input>
        </div>
        <div className="col"></div>
        <div className="col"></div>
      </div>
    </div>
  );
}

export default App;
