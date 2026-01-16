# GAMES

Unity URP demo scaffolding for a Fears-to-Fathom-inspired Yakut horror prototype.

## Unity Project Structure

`UnityProject/Assets/Scripts` contains the core gameplay scripts for the MVP demo:

- `FirstPersonController` (WASD + Shift run + mouse look)
- `Interactor` + `Interactable` base
- `DoorInteractable`, `LightSwitchInteractable`, `GeneratorInteractable`, `RadioInteractable`
- `SubtitleManager` for intro/subtitle beats
- `TriggerEvent` for scripted moments

These scripts are intended to be dropped into a Unity URP project and wired in the Inspector.
