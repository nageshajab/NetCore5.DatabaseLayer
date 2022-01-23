github repository: https://github.com/nageshajab/NetCore5.DatabaseLayer

Written in .Net core 5.

This package contains common methods to communicate with different databases. 

A developer does not need to know following things to interact with different databases. 
a. different nuget packages for different database
b. different class and methods for different database

All developer need to know is sql syntax for each database. This package will return data in desired format as command is passed from user.

Current functionalities supported:
1. GetDataset(CommandText)
2. ExecuteNonQuery(commandText)
3. ExecuteScalar(commandText) - return single object value

Right now it supports SQL server & SQLite. 
But future versions will contain support to Postgres, MySql, MongoDb, Elastic search.
