
--PreRequisite to give project strat details!



Trade Offs

1) Fast Startup & Slow lookups vs Slow Startup & Fast Lookups?  // I chose ( Slow Startup & Fast Lookups)
	
	-- I went with Slow Startup and Quick Lookups. (Hybrid model with saving data in a File and during startup,
		load them into Cache but once in cache, it will be very fast)

2) Reverse Logic vs Something to avoid Duplicacy   // I chose (Graphs)

	-- I went with Graphs as I came to know they work on Nodes and Edges and using them we can arrange data in a 
		BiDirectional way  (if A has relatonship with B, then if someone searches B, its all relationships will
		come up).
		It avoids duplicacy.


		-- Directed vs UnDirected
		--Weghted vs UnWeighted // like we can say happy is 2 time weighted to joyful, 3 times weighted to escatic and 
			1 time wieghted to glad
			// but for this interview I can go with unweighted as it can work as an extension for future releases!


3) React app vs HTML pages	 // I chose (HTML)

	-- I went with HTML so that I can focus more on functionality of Graph and Cache 
	-- It will be difficult to release 2 different tech stacks for a small project.


4) SQL vs NoSQL	vs FileSystem	// I chose (FileSystem)

	-- We do not need any ACID properties/ relationships between numerous tables, so we can avoid 
	Relational Databases.
	-- It will also be an overhead to setup DB and make unnecessary connections for this small project


5) Keeping DataReader so that in future, instea of file, it can ready from a DB 

6) record video of UI of full startup and functionality

7) 


🔍 If you're curious why this matters:
Without [HttpPost("AddWord")], your method is just:


[HttpPost]
And ASP.NET Core doesn't auto-match method names as routes unless you're using [Route("action")] in the controller base route, or older MVC-style config.



Security
we took care of initial validations 
SQL injections and Cross site


Security considerations like SQL injection protection, CSRF tokens, and advanced input sanitization are not included due to the current scope (no database, no auth).
However, those would be essential in a production setting.

“I used HttpPost for GetSynonyms to ensure clean validation using [FromBody] and avoid fragile query string parsing. 
Although it retrieves data, using POST here makes validation and future extensibility easier.”



for testing most important package -> aspneycore.mvc.testing