[简体中文](README.zh-CN.md)

# 2D Adventure

## Project Overview

A Unity 2D action-platformer exercise I completed following M Studio's [《勇士传说》 tutorial](https://learn.u3d.cn/tutorial/2DAdventure). Explore forests and caves, fight enemies, and interact with chests, checkpoints, and portals. This is tutorial-based learning practice.

## Gameplay & Controls

Run, walk, crouch, jump, wall-slide, wall-jump, and perform grounded melee attacks. Grounded slides consume regenerating energy; taking damage causes knockback and temporary invulnerability.

Bindings: [Input Actions](Assets/Settings/Input%20System/PlayerInputControl.inputactions) and gameplay scripts. Gamepad labels indicate button positions.

| Action | Keyboard | Gamepad |
| --- | --- | --- |
| Move horizontally | A / D or Left / Right arrows | Left stick |
| Crouch on ground | S or Down arrow | Left stick down |
| Jump / wall-jump | Space | South button |
| Walk modifier on ground | Hold Left Shift | Hold left trigger |
| Grounded attack | J | West button |
| Grounded slide | K | Left shoulder |
| Interact nearby | E | East button |
| Load saved state (script shortcut) | L | Not bound |

The settings button pauses play and offers volume adjustment. Chests open visually, without inventory or loot. Activate the cave checkpoint before loading.

## Technical Highlights & What I Learned

### 1. Input, animation, and combat timing

[PlayerController](Assets/Scripts/Player/PlayerController.cs) accepts grounded attack input and calls [PlayerAnimation](Assets/Scripts/Player/PlayerAnimation.cs). The [Animator Controller](Assets/Animations/Player/Player.controller) handles three attack animations; clips activate attack-area objects during hit windows. [Attack](Assets/Scripts/General/Attack.cs) forwards trigger contacts to [Character](Assets/Scripts/General/Character.cs), which applies damage, invulnerability, and hurt/death events. [AttackFinish](Assets/Scripts/Player/AttackFinish.cs) clears the attack flag on state exit. I learned to coordinate input, animation, hitbox timing, and feedback instead of keypress-driven damage.

### 2. Enemy finite-state machines

[Enemy](Assets/Scripts/Enemy/Enemy.cs) delegates logic and physics updates to state objects, switching through exit/enter callbacks. The boar's [patrol](Assets/Scripts/Enemy/BoarPatrolState.cs) uses BoxCast detection to enter [chase](Assets/Scripts/Enemy/BoarChaseState.cs), then returns after a lost-target timer. [Bee](Assets/Scripts/Enemy/Bee.cs) uses circular detection and aerial pursuit; the [snail skill state](Assets/Scripts/Enemy/SnailSkillState.cs) manages hiding and invulnerability. I learned to separate shared sensing, movement, and timers from species-specific behavior.

### 3. Event channels and scene lifecycle

[ScriptableObject event channels](Assets/Scripts/ScriptableObject/) carry scene, health, audio, and fade notifications; [GameSceneSO](Assets/Scripts/ScriptableObject/GameSceneSO.cs) stores scene type and an Addressables reference. [SceneLoader](Assets/Scripts/Transition/SceneLoader.cs) coordinates fades, unloading, additive loading, and player repositioning. Load events disable gameplay input; completion restores it in location scenes and refreshes camera bounds. I learned how shared event assets connect systems across scene boundaries without direct references between every participant.

### 4. Checkpoint persistence

[SavePoint](Assets/Scripts/Save%20Load/SavePoint.cs) raises a save event. [DataManager](Assets/Scripts/Save%20Load/DataManager.cs) collects registered [ISaveable](Assets/Scripts/Save%20Load/ISaveable.cs) objects and writes Newtonsoft JSON to `Application.persistentDataPath/SAVEDATA/data.sav`. Character IDs key position, health, and energy; the scene loader saves the current scene. I learned interface-based collection and restoration of selected state, not a complete world snapshot.

Other practice includes [ground/wall physics checks](Assets/Scripts/General/PhysicsCheck.cs), Tilemap collisions, URP 2D lighting/post-processing, [Cinemachine bounds and shake](Assets/Scripts/Utilities/CameraControl.cs), [health/energy UI](Assets/Scripts/UI/PlayerStatBar.cs), [AudioMixer volume control](Assets/Scripts/Audio/AudioManager.cs), and [DOTween fades](Assets/Scripts/UI/FadeCanvas.cs).

## Getting Started

1. Install **Unity Editor 2022.3.62f3c1**, matching [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt).
2. In Unity Hub, choose **Add / Add project from disk**, select the repository root, and open with that Editor. Wait for package restoration and import.
3. Select **Window > Asset Management > Addressables > Groups > Play Mode Script > Use Asset Database (fastest)** for local play without prebuilt bundles.
4. Open [Initialization.unity](Assets/Scenes/Initialization.unity) and enter Play Mode: Persistent loads, then Menu. Choose New Game for Forest; portals connect Forest and Cave.

Source/configuration inspected; a fresh Editor run remains unverified.

## Credits

- **M Studio**: [《勇士传说》 tutorial](https://learn.u3d.cn/tutorial/2DAdventure), the project's learning source; also the bundled [Colourful Hierarchy editor tool](Assets/M%20Studio/Colourful%20Hierarchy%20Category%20GameObject/).
- **Dustyroom**: Casual Game Sounds U6; the bundled [license](Assets/Audio/Casual%20Game%20Sounds%20U6/CasualGameSounds/license.pdf) explicitly grants **CC0** use.
- Included [art](Assets/Art%20Assets/): Generic-Character and Legacy-Fantasy-High Forest; [music](Assets/Audio/FREE%20CASUAL%20MUSIC%20PACK/): FREE CASUAL MUSIC PACK; [font](Assets/Fonts/SmileySans-Oblique.ttf): SmileySans-Oblique. Authors/source URLs and redistribution terms are undocumented locally; verify before redistribution.
- **Demigiant / Daniele Giardini**: DOTween; see the bundled [readme and license link](Assets/Plugins/Demigiant/DOTween/readme.txt). Third-party assets retain their respective terms; this README grants no additional license.