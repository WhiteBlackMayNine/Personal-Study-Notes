---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Playerprefs/PlayerPrefs存储位置
created: ""
---

---
# 关联知识点



---
# Windows

- PlayerPefs 存储在
	- `HKCU\Software\[公司名称]\[产品名称]项下的注册表中`
	- 其中公司和产品名称是 在“Project settings”中设置的名称

- 运行 regedit
- ——> HKEY CURRENT USER
- ——> SOFTWARE
- ——> Unity
- ——> UnityEditor
- ——> 公司名称
- ——> 产品名称
# Android

- `/data/data/包名/shared prefs/pkg-name.xml`
# ioS

- `/Library/Preferences/MHIDl.plist`
# 唯一性

- PlayerPrefs中不同数据的唯一性
- 是由key决定的，不同的key决定了不同的数据
- 同一项目中 如果不同数据key相同 会造成数据丢失
- 要保证数据不丢失就要建立一个保证 key唯一 的规则

---
