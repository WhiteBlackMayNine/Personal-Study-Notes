
- 简介
	- SQLite是一款轻型的数据库
	- SQLite的设计目标是嵌入式的
	- SQLite占用资源非常的低
	- SQLite能够支持Windows/Linux/Unix等等主流的操作系统
- SQL语法
	- INSERT INTO 语句
		- 作用
			- 用于向表格中插入新的行
		- 语法
			- `INSERT INTO 表名称 VALUES (值1, 值2,....)`
			- `INSERT INTO table_name (列1, 列2,...) VALUES (值1, 值2,....)`
		- 示例
			- `INSERT INTO Persons VALUES ('Gates', 'Bill', 'Xuanwumen 10', ‘Beijing')
			- `INSERT INTO Persons (LastName, Address) VALUES ('Wilson', 'Champs-Elysees')
	- DELETE 语句
		- 作用
			- DELETE 语句用于删除表中的行
		- 语法
			- `DELETE FROM Person WHERE LastName = 'Wilson'
		- 示例
			- `DELETE FROM Person WHERE LastName = 'Wilson'
			- `DELETE FROM table_name（删除表格所有内容）`
	- UPDATE 语句
		- 作用
			- Update 语句用于修改表中的数据
		- 语法
			- `UPDATE 表名称 SET 列名称 = 新值 WHERE 列名称 = 某值
		- 示例
			- `UPDATE Person SET FirstName = 'Fred' WHERE LastName = 'Wilson'`
	- SELECT 语句
		- 作用
			- SELECT 语句用于从表中选取数据
		- 语法
			- `SELECT 列名称 FROM 表名称
		- 示例
			- `SELECT LastName,FirstName FROM Persons

- Unity使用SQLite数据库
	- 准备工作
		- 导入mono.data.sqlite.dll到Assets文件夹
		- 代码添加库：using Mono.Data.Sqlite
	- 数据连接
		- ![[SQLiteForUnity数据连接.png]]
	- 建表
		- ![[SQLiteForUnity建表.png]]
		- ![[SQLiteForUnity建表2.png]]
		- ![[SQLiteForUnity建表3.png]]
	- 插入数据
		- ![[SQLiteForUnity插入数据.png]]
	- 查询数据
		- ![[SQLiteForUnity查询数据.png]]
		- ![[SQLiteForUnity查询数据2.png]]
	- 执行SQL语句三种方式
		- ![[SQLiteForUnity执行SQL语句三种方式.png]]

- 数据库管理类
	- 封装优点
		- 方便项目管理
		- 方便开发人员的快捷的使用
		- 防止高度保密数据外泄
	- 步骤
		- 连接数据库
			- ![[数据库管理类连接数据库.png]]
			- ![[数据库管理类连接数据库2.png]]
		- 通过Sql语句插入数据
			- ![[数据库管理类通过Sql语句插入数据.png]]
		- 通过Sql语句查询
			- ![[数据库管理类通过Sql语句查询.png]]
		- 通过表名查询所有数据
			- ![[数据库管理类通过表名查询所有数据.png]]

- 不同平台选择不同的存储路径
	- 平台—路径
		- ![[平台—路径.png]]
	- Application.dataPath
		- 在直接使用Application.dataPath来读取文件进行操作，移动端是没有访问权限的
	- Application.streamingAssetsPath
		- 直接使用Application.streamingAssetsPath来读取文件进行操作，此方法在pc/Mac电脑中可实现对文件实施“增删查改”等操作
		- 但在移动端只支持读取操作
	- Application.persistentDataPath
		- 使用Application.persistentDataPath来操作文件，该文件存在手机沙盒中，因为不能直接存放文件
		- 通过服务器直接下载保存到该位置，也可以通过Md5码比对下载更新新的资源
		- 没有服务器的，间接通过文件流的方式从本地读取并写入Application.persistentDataPath文件下，然后再通过Application.persistentDataPath来读取操作
		- 注意
			- 在Pc/Mac电脑 以及android跟Ipad、ipone都可对文件进行任意操作，另外在IOS上该目录下的东西可以被iCloud自动备份








`


