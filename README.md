![TerminalGif](https://github.com/jacobjea/Terminal/assets/89589209/8fcd4df9-0892-4f2c-a223-5a50030a50f5)

# Terminal System

Terminal System is a tool that lets you invoke registered functions from anywhere.<br/>
Lists the functions that are most similar to the entered value first.

## How to install

![image](https://github.com/jacobjea/Terminal/assets/89589209/12177e2e-fd05-4979-9e75-ea8d57a59748)

Download the project and place it under the unity **Assets** folder
<br/><br/><br/>

## How it works


```C#
MethodInfo[] methods = terminalFunc.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
```

**TerminalSystem** uses **Reflection** to get methodinfo.
<br/><br/><br/>

```C#
#if (UNITY_EDITOR)
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Init()
        {
            GameObject terminalSystem = new GameObject("TerminalSystem");
            terminalFunc = terminalSystem.AddComponent<TerminalFunc>();
            terminalFunc.Init();
        }
#endif
```

Automatically generates the editor when it plays. There is no need to place anything in the scene!
<br/><br/><br/>

![image](https://github.com/jacobjea/Terminal/assets/89589209/d481d522-eb6a-49b0-be2a-27e7cd0e960e)

You can enable or disable the console window by typing **ALT + ENTER**
<br/><br/><br/>

![image](https://github.com/jacobjea/Terminal/assets/89589209/3042852f-a6d4-4e3a-b22f-4965686a3e6f)

Just write a function in the **TerminalFunc.cs** class and add the **[Terminal]** attribute!<br/>
After that, it can be confirmed that the function is registered in the terminal.



## Final words

I hope my project will help develop efficiently!
