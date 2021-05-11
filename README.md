# Linq2dbBulkCopyIssue
Demonstrates an issue with Linq2db BulkCopy and EF Core where pre-existing Guids are saved differently. 

To generate the sqlite db files, run the tests, then head into the `Linq2dbBulkCopyIssue\bin\Debug\net5.0` folder and inspect `ef.db` and `linq2db.db` in a viewer like [this](https://inloop.github.io/sqlite-viewer/) one. The difference in the way the Guids are saved as `TEXT` is evident. 

Sample db files are provided for:
- [EF Core](https://github.com/Nate129/Linq2dbBulkCopyIssue/blob/master/ef.db)
- [Linq2db](https://github.com/Nate129/Linq2dbBulkCopyIssue/blob/master/linq2db.db) 

Screenshots are available in the repo as well of the sample files viewed in the viewer linked above: 
- [EF Core](https://github.com/Nate129/Linq2dbBulkCopyIssue/blob/master/ef.PNG) 
- [Linq2db](https://github.com/Nate129/Linq2dbBulkCopyIssue/blob/master/linq2db.PNG)
