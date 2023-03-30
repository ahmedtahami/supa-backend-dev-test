# supa-backend-dev-test

### Starting the Project

1. Restore the DB Backup using `SupaQuizDB.bak` file in root folder.
2. Open project in Visual Studio and execute following command on Package Manager Console

   `Scaffold-DbContext "YOUR_CONNECTION_STRING" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context QuizDbContext --force`
3. And that's it you are good to go!
   
