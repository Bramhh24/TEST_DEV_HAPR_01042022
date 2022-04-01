# TEST_DEV_HAPR_01042022
Prueba Toka y FrontWeb utilice como paquetes de nuget Dapper y System.Data.SqlClient. El proyecto PruebaToka es el Rest Api.

En la base de datos hice ligeros cambios y en los procedimientos almacenados no me funcionaban el actualizar y borrar, porque entraba al if
y mandaba el msj de error y nunca aplicaba el update, unicamente los cambie de lugar.

Para ser sincero nunca había hecho ni una api y tampoco había trabajado haciendo un login con su logout y crud. 
Unicamente he hecho cruds y otras funciones pero sin la necesidad de tener un usuario conectado. 
Así que todo esto fue mi primera vez y espero este bien. Probé el api usando el Swagger y todo me daba bien.

Le agregue validaciones al modelo del api y hice una validación para el RFC llamado TrecePalabras que se encuentra en la carpeta Validaciones. 
En la carpeta Servicios es donde tengo la clase para la conexion de la base de datos y ahí se encuentran los metodos para llamar los procedimientos almacenados.

Sobre el FrontWeb hice todo el login, logout como su registro, actualizar y borrar. Pero me falto consumir el api para mostrar el modulo de reportes, 
sinceramente porque ya no supe como consumir el api, ya que todo esto es nuevo para mi. Igual tiene validaciones y traduje los mensajes de error que manda identity,
la clase se encuentra en la carpeta Servicios. Y ya en la clase Program deshabilité algunos requisitos de contraseña y 
habilité que nombre de usuario pueda tener espacio, ya que lo uso como nombre.
