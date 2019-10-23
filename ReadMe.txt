1. Системные требования
ОС: Windows с установленным IIS. 
К IIS необходимо установленное дополнение Runtime & Hosting Bundle 2.2.0
Наличие MS SQL Server 2014 и старше

2. Конфигурирование и Запуск приложения
Для развертывания приложения вам понадобятся следующие файлы из :
https://github.com/EugeneSvetlakov/MicrowaveQueueWebApp/tree/master/Distrib
- dotnet-hosting-2.2.0-win.exe (дополнение к IIS для поддержки работы с проектовами на ASP.NET Core)
- win-x86-exe.zip (архив приложения)

2.1 При необходимости установить Runtime & Hosting Bundle 2.2.0 (dotnet-hosting-2.2.0-win.exe)

2.2 Развернуть содержимое архива win-x86-exe.zip в желаемую папку (по умолчанию: C:\inetpub). 
Например: C:\inetpub\MvQueue
Проверить что у группы IIS_IUSRS есть полный доступ к папке (Doc\img\FolderPermission.jpg).

2.3 Настройка подключения приложения к Базе Данных
Отредактируйте строку "DefaultConnection" в файле C:\inetpub\MvQueue\appsettings.json

Замените: 
- SqlServer на адрес вашего MS SQL сервера, например: ServerDnsName\\SQLEXPRESS
- DbName на имя БД
- DbUSer на имя пользователя обладающего необходимыми правами на БД DbName
- DbUserPasswd на пароль DbUSer'a

2.4 Настройка IIS:
- Запустить Диспетчер служб IIS (Internet Information Services (IIS) Manager).
Doc\img\ManagerIIS.jpg
- Добавить новый сайт:  Doc\img\IIS_01.jpg
- Заполняем поля согласно примера: Doc\img\IIS_02.jpg
	,где: Физический путь - путь к файлу "web.config" ("appsettings.json"),
- Добавляем ssl(https): Doc\img\IIS_0[3-5].jpg
	, выбранный сертификат должен быть выпущен для узла, указанного на предыдущем шаге
- Изменим настройку пула приложения: Doc\img\IIS_06.jpg, "версия среды" установить: "без управляемого кода"

Если все параметры указаны верно и есть права и доступ к базе данных
	- при первом открытии сайта произойдет первичное наполнение БД и вам предстанет 
		Doc\img\ApplicationSuccesStarted.jpg
