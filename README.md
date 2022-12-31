# BlazorServerCookieAuthentication-master
A cookie based authentication sample for BlzaorServer without using the Identity system.

Special thanks to <a href="https://github.com/VahidN/ASPNETCore2CookieAuthentication">VahidN</a>. This project has used his project <a href="https://github.com/VahidN/ASPNETCore2CookieAuthentication">ASPNETCore2CookieAuthentication</a>.  

It includes:

Users and Roles tables with a many-to-many relationship.

A separated EF Core data layer with enabled migrations.

An EF Core 7x based service layer.

A Db initializer to seed the default database values.

A User and Role Services with cookie and DB based login and logout capabilities.
