# CyptoWallet Authentication and Autorization  

**Serverside**: ASP.Net Core API

**Clientside**: Reactjs

## Getting Started

Clone the repository to your local machine or download project zip file to your local machine.

For running this project you need the following items to Installed on your local machine

### Serverside Prerequisites

* [Microsoft Visual Studio 2022](https://visualstudio.microsoft.com/vs/) - IDE
* [Dotnet 6 ](https://maven.apache.org/) - Framework
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2019) - DataBase Engine for store data

### Clientside Prerequisites

* [Microsoft Visual Studio Code](https://code.visualstudio.com/download) - IDE
* [NPM](https://nodejs.org/en/download/) - Package Manager
* [MetaMask](https://metamask.io/) - Metamask Browser Extension for Signing Message


### Serverside Installing

Run the below command to restore, build and run the project

Restore the nuget packages
```
dotnet restore
```

Buid the project

```
dotnet build
```
Add or Update DataBase (Package Manager Console)

```
update-database
```
Run Server Project

```
dotnet run
```

### Clientside Installing

Run the below command to install dependencies and run project

Install Dependencies
```
npm install
```

Run Client Project

```
npm start
```


## Deployment

This project is a easy sample for Athentication with Crypto Wallet and there is no need to Deployed

## Authors

* **Mohsen Asadi** - *Initial work* - [MohsenAsadi](https://github.com/mohsenasadi501)


## License

This project is licensed under the MIT License

## Acknowledgments

* Storing data in SQL
* JWT AccessToken & RefreshToken
* Reactjs - web3 - etherjs
* Nethereum
