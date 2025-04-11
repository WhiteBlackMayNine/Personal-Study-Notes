---
tags:
  - "#科学/Unity/ARPG/Combo/CharacterComboDataSO"
created: 2024-06-20
---

---
# 关联知识点

[[CharacterComboSO]]  

---
# 说明

- 利用 [[Unity 知识点#^afe6ee|ScriptableObject]] 完成动作信息配置 模块
- 以后使用的时候拿模块出来配一下就行
- 这个脚本写的是招式的基本信息
	- 另外一个（`CharacterComboSO`）写的是连招表信息
# 变量

- `string _comboName
	- 招式的名字
		- 要播放的某个动画，只需要去获取这个动画的名字
- `string[] _comboHitName`
	- 受击动画组
	- 用于应对一个攻击动画需要 不同 / 多个 受击动画
- `string[] _comboParryName`
	- 格挡动画组
	- 作用与上一个相同
- `float _damage`
	- 伤害
- `float _coldTime`
	- 下一次攻击的间隔时间
	- 需要 计时器 来完成
- `float _comboPositionOffset`
	- 偏移量
	- 用于让角色与目标保持一定的距离，防止出现穿模现象
## Lambad表达式

- 将上述变量联结一下，方便外部（`CharacterComboSO`）调用
- 同时获取一下最大受伤/格挡数
-  [[4 Lambad表达式|=> / ()=>]]
```C#
public string ComboName => _comboName;
public string[] ComboHitName => _comboHitName;
public string[] ComboParryName => _comboParryName;
public float Damage => _damage;
public float ColdTime => _coldTime;
public float ComboPositionOffset => _comboPositionOffset;

public float GetComboMaxHitandParryCount()=> _comboHitName.Length;
```

# 图片

- ![[CharacterComboDataSO_1.png]]
# 更新

- 记录连招的基本信息

```C#
[SerializeField] private string _comboName;
[SerializeField] private string[] _comboHitName;
[SerializeField] private string[] _comboParryName;
[SerializeField] private float _damage;
[SerializeField] private float _coldTime;
[SerializeField] private float _comboPositionOffset;

public string ComboName => _comboName;
public string[] ComboHitName => _comboHitName;
public string[] ComboParryName => _comboParryName;
public float Damage => _damage;
public float ColdTime => _coldTime;
public float ComboPositionOffset => _comboPositionOffset;

public float GetComboMaxHitandParryCount ()=> _comboHitName.Length;
```

- 招式动画名字
- 对应的受击/格挡动画名字
- 造成的伤害
- 冷却时间
- 攻击位置偏移量
	- 用于 [[CharacterCombatBase]] 的 位置匹配

- `GetComboMaxHitandParryCount`
	- 受伤和格挡动画的数量一定是一致的，所以只需要关联其中一个的长度就行

- 创建出的 CharacterComboDataSO 的资产文件 要挂载到 CharacterComboSO 创建出的资产文件上
- 前者创建出来的是一个招式，而后者创建出来的是一个连招表

---
# 源代码

![[CharacterComboDataSO.cs]]

---