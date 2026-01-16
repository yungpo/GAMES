# GAMES

Unity URP demo scaffolding for a Fears-to-Fathom-inspired Yakut horror prototype.

## Unity Project Structure

`UnityProject/Assets/Scripts` contains the core gameplay scripts for the MVP demo and a runtime bootstrap that builds a playable scene with primitive geometry:

- `FirstPersonController` (WASD + Shift run + mouse look)
- `Interactor` + `Interactable` base
- `DoorInteractable`, `LightSwitchInteractable`, `GeneratorInteractable`, `RadioInteractable`
- `MapInteractable`, `TalismanInteractable`
- `DemoSequenceController` (demo story beats/objectives)
- `DemoBootstrap` (auto-builds a playable demo scene at runtime)
- `SubtitleManager` for intro/subtitle beats
- `PromptUI`, `ObjectiveUI`
- `TriggerEvent` for scripted moments

These scripts are intended to be dropped into a Unity URP project and wired in the Inspector.
`DemoBootstrap` allows you to press Play in any empty scene and get a functional MVP demo scene without manual setup.
TextMeshPro (TMP) is required for subtitles and UI prompts.
