# BlazorServerCookieAuthentication-master
It's a test project with BlazorServer that use a light cookie authenticator instead of Microsoft Identity.

Special thanks to <a href="https://github.com/VahidN/ASPNETCore2CookieAuthentication">VahidN</a>. This project has used his project <a href="https://github.com/VahidN/ASPNETCore2CookieAuthentication">ASPNETCore2CookieAuthentication</a>.  

It includes:

Users and Roles tables with a many-to-may relationship.

A separated EF Core data layer with enabled migrations.

An EF Core 6.0.102 based service layer.

A Db initializer to seed the default database values.

An account controller with cookie and DB based login and logout capabilities.
