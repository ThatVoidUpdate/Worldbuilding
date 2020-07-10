using UnityEngine.Events;

[System.Serializable]
public class StringEvent : UnityEvent<string> { } // Can pass a string as an argument in the event
[System.Serializable]
public class BoolEvent : UnityEvent<bool> { } // Can pass a bool as an argument in the event