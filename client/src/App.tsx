import React from "react";
import "./App.css";
import axios from "axios";
import { ethers } from "ethers";
import Web3Modal from "web3modal";

function App() {
  const baseURL = "https://localhost:7061/";
  const [nonce, setNonce] = React.useState("");
  const [token, setToken] = React.useState("");
  const [account, setAccount] = React.useState("");
  const [network, setNetwork] = React.useState("");
  const [chainId, setChainId] = React.useState(0);
  const [signature, setSignature] = React.useState("");

  async function connect() {
    if (typeof window !== "undefined") {
      const web3Modal = new Web3Modal();
      const connection = await web3Modal.connect();
      const provider = new ethers.providers.Web3Provider(connection);
      const signer = provider.getSigner();
      const address = await signer.getAddress();
      const networkData = await provider.getNetwork();
      console.log(nonce)
      let mySignature = await signer.signMessage(nonce.toString());

      setSignature(mySignature);
      setAccount(address);
      setNetwork(networkData.name);
      setChainId(networkData.chainId);
    }
  }
  function getNonnce() {
    let uri = baseURL + "User/nonce";
    axios
      .get(uri, {
        params: { publicAddress: "0x174A639d18a2EE6590ed1A201F8CCC76A52FFB13" },
      })
      .then((response) => {
        console.log(response);
        setNonce(response.data);
      });
  }
  function login() {
    let uri = baseURL + "User/authenticate";
    const json = JSON.stringify({
      publicAddress: "0x174A639d18a2EE6590ed1A201F8CCC76A52FFB13",
      signature: signature,
    });
    axios
      .post(uri, json, {
        headers: {
          "Content-Type": "application/json",
        },
      })
      .then((response) => {
        localStorage.setItem("token", response.data.accessToken);
        localStorage.setItem("refreshToken", response.data.accessToken);
        setToken(response.data.accessToken);
      });
  }

  return (
    <div className="container">
      <div className="row">
        <div className="col">
          <br></br>
          <div className="input-group mb-3">
            <button
              type="button"
              className="btn btn-primary"
              onClick={getNonnce}
            >
              Get Nonce
            </button>
            <input type="text" value={nonce}></input>
          </div>
          <div className="input-group mb-3">
            <button type="button" className="btn btn-primary" onClick={login}>
              Login
            </button>
            <input type="text" value={token}></input>
          </div>
        </div>
        <div className="col">
          <br></br>
          <div className="input-group mb-3">
            <button type="button" className="btn btn-primary" onClick={connect}>
              Connect to MetaMask
            </button>
          </div>
          <p>Address:</p>
          <p>{account}</p>
          <p>Network Name:</p>
          <p>{network}</p>
          <p>ChainId:</p>
          <p>{chainId}</p>
        </div>
      </div>
    </div>
  );
}

export default App;
