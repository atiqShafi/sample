# Sample CRUD application
### For demonstration purposes only

## Techniques
- Asp.net MVC 5.2.3
- Entity Framework 6.1.3
- Kendo UI (grid)
- SimpleInjector (ioc)
- Elmah,nLog (logging)
- Dotless (less)
- AngularJS 1.3
- AngularUI router

## Prerequisites
Visual studio 2013+
MS Sql 2008R2+

Create dev.connection.strings.config in Sample.Web root
```
<connectionStrings>
  <add name="AppDb" connectionString="Data Source=localhost;Initial Catalog=SampleApp;User ID=userid;Password=password;MultipleActiveResultSets=true" providerName="System.Data.SqlClient" />
</connectionStrings> 
```

Run application (migrations run on start)
