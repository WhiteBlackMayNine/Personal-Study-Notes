---
tags:
  - "#Unity"
  - "#Polygon"
  - "#猎魔人"
created: 2024-12-06
---

多边形碰撞器 PolygonCollider2D

`_collider2D.points.Length`

Point属性的 Element 的 Size
也就是一条路径的 点 的个数

`_collider2D.pathCount`

Point 属性道德 Size
路径的个数

`_collider2D.SetPath(0, _points);`

设置碰撞器的路径（也就是设置碰撞器的形状
参数一  路径的索引 
参数二 `Vector2[]` 类型的数组（也就是顶点的位置


多条路径
就相当于用了两个线条勾勒一个形状
用于整复杂的图形
或者当一个单线条的勾勒的形状顶点数过多时，使用多路径进行勾勒（节约性能）

`_collider2D.points = _points;`

直接赋值，适用于单线条勾勒的图形




