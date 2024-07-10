# Copper Framework

> it does game stuff

## Features

- Component System
- Custom DearImGui layer
- Scene system
- Dev tools

## To Do

- [ ] Camera Component
- [ ] Ui System
    - [X] Box
    - [ ] Scaling Elements
    - [ ] Button
    - [ ] Text
- [ ] Physics System
- [ ] Sounds
    - [ ] Sound play component
    - [ ] Sound object
- [ ] Renderer system
    - [ ] Raylib renderer
    - [ ] Headless renderer

## Used Libraries

- [Raylib-CSharp](https://www.nuget.org/packages/Raylib-CSharp)
- [Aether.Physics2D](https://www.nuget.org/packages/Aether.Physics2D)
- [CopperDevs.Core](https://www.nuget.org/packages/CopperDevs.Core)
- [DeepCloner](https://www.nuget.org/packages/DeepCloner)
- [ImGui.NET](https://www.nuget.org/packages/ImGui.NET)

## Basic Example

```csharp
    public static void Main()
    {
        var engine = new Engine(EngineSettings.Development);

        engine.OnLoad += OnEngineLoad;

        engine.Run();
    }

    private static void OnEngineLoad()
    {
        Log.Info("Engine and all its components are loaded");
        Log.Info("Here is where you would load ui, sounds, and sprites");
    }
```