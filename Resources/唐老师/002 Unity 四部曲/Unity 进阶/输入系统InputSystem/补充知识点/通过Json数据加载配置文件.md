---
tags:
  - 科学/Unity/唐老狮/Unity进阶/输入系统InputSystem/补充知识点/通过Json数据加载配置文件
created: 2024-07-31
更新:
---

---
# 关联知识点



---
# 通过 Json 手动加载输入配置文件

- 相当于 通过代码的形式，去加载了一个 JSON 文件
- 根据 JSON 文件里面的信息，生成了一个对应的类对象
- 然后把这个类对象赋值给 PlayerInput 脚本中的 Actions
- PlayerInput 里面输入相关的这些数据其实就通过这个对象获取成功了
# 重要API

- `InputActionAsset.FromJson(json);`
# 代码示例


```C#
public PlayerInput input;

void Start(){
	//PlayerInputTest 为一个存储 输入信息的 text 文件
	string json = Resources.Load<TextAsset>("PlayerInputTest").text;
	//生成 InputActionAsset 文件
	InputActionAsset asset = InputActionAsset.FromJson(json);
	
	input.actions = asset;
	
	input.onActionTriggered +=(context)=>{
	
		if(context.phase == InputActionPhase.Performed){
			
			switch(context.action.name){
				case "Move" :
					print("移动");
					break;
				case "Look":
					print("看向");
					break;
				case "Fire":
					print("开火");
					break;
			}
		}
	}
}
```

---
