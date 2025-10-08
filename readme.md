# VadyaRus Events

A simple and lightweight event bus system for Unity, designed for decoupled communication between different parts of an application.

## 🚀 Features

* **Decoupled Communication:** Allows different systems to communicate without direct references.
* **Simple API:** Easy to register, unregister, and raise events.
* **Type Safe:** Uses generics to ensure that event listeners only receive events of the correct type.
* **Automated Initialization:** Automatically finds all `IEvent` implementations and initializes the corresponding event buses.

## 📦 Installation

You can add this package to your Unity project using the Package Manager.

1.  In the Unity Editor, go to **Window > Package Manager**.
2.  Click the **"+"** icon in the top-left corner.
3.  Select **"Add package from git URL..."**.
4.  Enter the following URL:

    ```
    https://github.com/vadyarus/events.git
    ```

5.  Click **Add**.

##  usage

### 1. Create an Event

First, create a new class that implements the `IEvent` interface. This class can contain any data that you want to pass along with the event.

```csharp
using VadyaRus.Events;

public struct PlayerDiedEvent : IEvent {
    public string PlayerName { get; }
    public int Score { get; }

    public PlayerDiedEvent(string playerName, int score) {
        PlayerName = playerName;
        Score = score;
    }
}
```

### 2. Register an Event Listener
To listen for an event, you need to register a method with the `EventBus`.
```csharp
using UnityEngine;
using VadyaRus.Events;

public class ScoreManager : MonoBehaviour {
    private EventBinding<PlayerDiedEvent> playerDiedBinding;

    private void OnEnable() {
        playerDiedBinding = new EventBinding<PlayerDiedEvent>(OnPlayerDied);
        EventBus<PlayerDiedEvent>.Register(playerDiedBinding);
    }

    private void OnDisable() {
        EventBus<PlayerDiedEvent>.Unregister(playerDiedBinding);
    }

    private void OnPlayerDied(PlayerDiedEvent e) {
        Debug.Log($"{e.PlayerName} died with a score of {e.Score}");
    }
}
```

### 3. Raise an Event
```csharp
using UnityEngine;
using VadyaRus.Events;


public class Player : MonoBehaviour {
    public void Die() {
        var playerDiedEvent = new PlayerDiedEvent("Player 1", 100);
        EventBus<PlayerDiedEvent>.Raise(playerDiedEvent);
    }
}
```

📜 License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/vadyarus/Events/tree/main?tab=MIT-1-ov-file) file for details.

