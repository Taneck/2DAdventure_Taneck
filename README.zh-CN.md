[English](README.md)

# 2D Adventure

## 项目简介

这是我跟随 M Studio 的 [Unity 教程《勇士传说》](https://learn.u3d.cn/tutorial/2DAdventure)完成的 2D 横版动作游戏练习。玩家在森林与洞穴关卡中探索、与敌人战斗，并与宝箱、存档点和传送点交互。本仓库用于展示教程学习实践。

## 玩法与操作

玩家可以奔跑、慢走、下蹲、跳跃、贴墙下滑、蹬墙跳，以及在地面进行近战攻击。地面滑铲消耗会自动恢复的能量；受击后会产生击退和短暂无敌。

下表依据[输入配置](Assets/Settings/Input%20System/PlayerInputControl.inputactions)及玩法脚本整理。手柄按键以位置描述，避免不同手柄的字母标识差异。

| 操作 | 键盘 | 手柄 |
| --- | --- | --- |
| 左右移动 | A / D 或左 / 右方向键 | 左摇杆 |
| 地面下蹲 | S 或下方向键 | 左摇杆向下 |
| 跳跃 / 蹬墙跳 | Space | 下侧面键 |
| 地面慢走修饰键 | 按住 Left Shift | 按住左扳机 |
| 地面攻击 | J | 左侧面键 |
| 地面滑铲 | K | 左肩键 |
| 与附近物体交互 | E | 右侧面键 |
| 读取存档状态（脚本快捷键） | L | 未绑定 |

点击屏幕上的设置按钮可暂停并调整音量。宝箱交互只切换开启外观，不代表实现了背包或掉落系统。读档前应先激活洞穴中的存档点。

## 技术亮点与学习收获

### 1. 输入、动画与攻击时序

[PlayerController](Assets/Scripts/Player/PlayerController.cs)接收地面攻击输入，调用 [PlayerAnimation](Assets/Scripts/Player/PlayerAnimation.cs)。[Animator Controller](Assets/Animations/Player/Player.controller)组织三段攻击动画，动画片段在命中窗口激活对应的攻击区域物体。[Attack](Assets/Scripts/General/Attack.cs)将触发器接触传给 [Character](Assets/Scripts/General/Character.cs)，由后者处理扣血、无敌以及受击和死亡事件。[AttackFinish](Assets/Scripts/Player/AttackFinish.cs)在退出动画状态时清除攻击标记。我通过这条链路学习了如何协调输入、动画状态、判定框生效时间与反馈，而不是按下按键就直接扣血。

### 2. 敌人有限状态机

[Enemy](Assets/Scripts/Enemy/Enemy.cs)将逻辑更新和物理更新委派给状态对象，切换时依次调用退出和进入回调。野猪在[巡逻状态](Assets/Scripts/Enemy/BoarPatrolState.cs)通过 BoxCast 感知玩家后进入[追击状态](Assets/Scripts/Enemy/BoarChaseState.cs)，丢失目标一段时间后返回巡逻。[蜜蜂](Assets/Scripts/Enemy/Bee.cs)使用圆形感知与空中追击；[蜗牛技能状态](Assets/Scripts/Enemy/SnailSkillState.cs)负责缩壳和无敌。我学习了将通用感知、移动和计时逻辑与不同敌人的专属行为分开。

### 3. 事件通道与场景生命周期

[ScriptableObject 事件通道](Assets/Scripts/ScriptableObject/)传递场景、生命值、音频和淡入淡出通知；[GameSceneSO](Assets/Scripts/ScriptableObject/GameSceneSO.cs)保存场景类型与 Addressables 引用。[SceneLoader](Assets/Scripts/Transition/SceneLoader.cs)协调淡入淡出、旧场景卸载、新场景叠加加载及玩家位置调整。加载事件禁用玩法输入，地点场景加载完成后恢复输入并刷新相机边界。我学习了通过共享事件资产连接跨场景系统，避免每个参与者都直接引用其他组件。

### 4. 存档点与持久化

[SavePoint](Assets/Scripts/Save%20Load/SavePoint.cs)发出存档事件，[DataManager](Assets/Scripts/Save%20Load/DataManager.cs)收集已注册的 [ISaveable](Assets/Scripts/Save%20Load/ISaveable.cs) 对象，以 Newtonsoft JSON 写入 `Application.persistentDataPath/SAVEDATA/data.sav`。角色 ID 用于索引位置、生命值和能量，场景加载器保存当前场景。我学习了通过接口统一收集与恢复选定状态；这里并非完整的世界快照存档。

其他实践包括[地面与墙体物理检测](Assets/Scripts/General/PhysicsCheck.cs)、Tilemap 碰撞、URP 2D 光照与后处理、[Cinemachine 边界及震屏](Assets/Scripts/Utilities/CameraControl.cs)、[生命值与能量 UI](Assets/Scripts/UI/PlayerStatBar.cs)、[AudioMixer 音量控制](Assets/Scripts/Audio/AudioManager.cs)，以及 [DOTween 淡入淡出](Assets/Scripts/UI/FadeCanvas.cs)。

## 运行方式

1. 安装 **Unity Editor 2022.3.62f3c1**，与 [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt) 中记录的版本完全一致。
2. 在 Unity Hub 中选择 **Add / Add project from disk**，添加仓库根目录，使用上述版本打开，等待依赖恢复与资源导入。
3. 本地进入 Play Mode 前，在 **Window > Asset Management > Addressables > Groups > Play Mode Script** 中选择 **Use Asset Database (fastest)**，避免依赖预先构建的资源包。
4. 打开 [Initialization.unity](Assets/Scenes/Initialization.unity)，进入 Play Mode。启动流程先加载 Persistent，再进入 Menu；选择 New Game 后进入 Forest，传送点连接 Forest 与 Cave。

以上步骤依据源码和配置核对，本次文档更新未验证全新环境下的 Editor 实际运行。

## 致谢与资源来源

- **M Studio**：[Unity 教程《勇士传说》](https://learn.u3d.cn/tutorial/2DAdventure)是本项目的学习来源；仓库还包含其 [Colourful Hierarchy 编辑器工具](Assets/M%20Studio/Colourful%20Hierarchy%20Category%20GameObject/)。
- **Dustyroom**：Casual Game Sounds U6 音效包；随附的[授权文件](Assets/Audio/Casual%20Game%20Sounds%20U6/CasualGameSounds/license.pdf)明确采用 **CC0**。
- 仓库内的[美术包](Assets/Art%20Assets/)包括 Generic-Character、Legacy-Fantasy-High Forest；[音乐包](Assets/Audio/FREE%20CASUAL%20MUSIC%20PACK/)为 FREE CASUAL MUSIC PACK；[字体](Assets/Fonts/SmileySans-Oblique.ttf)为 SmileySans-Oblique。本地未记录这些资源的作者、来源网址及再分发条款，重新分发前需核实。
- **Demigiant / Daniele Giardini**：DOTween，详见随附的[说明及授权链接](Assets/Plugins/Demigiant/DOTween/readme.txt)。第三方资源遵循各自条款，本 README 不授予额外许可。
