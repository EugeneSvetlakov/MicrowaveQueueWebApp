1. ��������� ����������
��: Windows � ������������� IIS. 
� IIS ���������� ������������� ���������� Runtime & Hosting Bundle 2.2.0
������� MS SQL Server 2014 � ������

2. ���������������� � ������ ����������
��� ������������� ���������� ��� ����������� ��������� ����� �� :
https://github.com/EugeneSvetlakov/MicrowaveQueueWebApp/tree/master/Distrib
- dotnet-hosting-2.2.0-win.exe (���������� � IIS ��� ��������� ������ � ����������� �� ASP.NET Core)
- win-x86-exe.zip (����� ����������)

2.1 ��� ������������� ���������� Runtime & Hosting Bundle 2.2.0 (dotnet-hosting-2.2.0-win.exe)

2.2 ���������� ���������� ������ win-x86-exe.zip � �������� ����� (�� ���������: C:\inetpub). 
��������: C:\inetpub\MvQueue
��������� ��� � ������ IIS_IUSRS ���� ������ ������ � ����� (Doc\img\FolderPermission.jpg).

2.3 ��������� ����������� ���������� � ���� ������
�������������� ������ "DefaultConnection" � ����� C:\inetpub\MvQueue\appsettings.json

��������: 
- SqlServer �� ����� ������ MS SQL �������, ��������: ServerDnsName\\SQLEXPRESS
- DbName �� ��� ��
- DbUSer �� ��� ������������ ����������� ������������ ������� �� �� DbName
- DbUserPasswd �� ������ DbUSer'a

2.4 ��������� IIS:
- ��������� ��������� ����� IIS (Internet Information Services (IIS) Manager).
Doc\img\ManagerIIS.jpg
- �������� ����� ����:  Doc\img\IIS_01.jpg
- ��������� ���� �������� �������: Doc\img\IIS_02.jpg
	,���: ���������� ���� - ���� � ����� "web.config" ("appsettings.json"),
- ��������� ssl(https): Doc\img\IIS_0[3-5].jpg
	, ��������� ���������� ������ ���� ������� ��� ����, ���������� �� ���������� ����
- ������� ��������� ���� ����������: Doc\img\IIS_06.jpg, "������ �����" ����������: "��� ������������ ����"

���� ��� ��������� ������� ����� � ���� ����� � ������ � ���� ������
	- ��� ������ �������� ����� ���������� ��������� ���������� �� � ��� ���������� 
		Doc\img\ApplicationSuccesStarted.jpg
