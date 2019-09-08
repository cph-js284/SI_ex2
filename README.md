# SI_ex2
This is the handin for the 2nd assignment for SystemIntegration for PBA SOFT2019fall

# What is it
This is a project using [GRpc](https://grpc.io/) written in C# (dotnetcore:target 2.2). Both the server and the client are setup to run in docker containers. 

# How to make it go

1) Clone the repo.
2) Start a container with a MySql-database:
```
sudo docker run -d --name mysql01 -p 3306:3306 -e MYSQL_ROOT_PASSWORD=test1234 mysql
```
### NB. You might have to wait for 10'ish secs. for the database to spinup inside the container before executing the server container
3) Navigate to the server folder. Build the GRpc-server container:
```
sudo docker build -t grpcserver .
```
4) Start the server:
```
sudo docker run -it --rm -p 6789:6789 --link mysql01 grpcserver
```
5) In a new terminal. Navigate to the client folder. Build the GRpc-client container:
```
sudo docker build -t grpcclient .
```
6) Start the client. The client is using GRpc to make the server query the data and return it. Running this command will out 8 lines to the console, 4 from a the textfile, 4 from the database:
```
sudo docker run --rm grpcclient
```

# Clean up
To remove the mysql-container:
```
sudo docker rm -f mysql01
```

# Additional
Ideally these container would/should be run from a docker-compose file.
Both the text-file and the database contains the fields:
```
ID(database only)   Name    Email   Address   Storagetype(used only for output)
```
Incase the addresses used didn't give it away - all info used is made up ;)
