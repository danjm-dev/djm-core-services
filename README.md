<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>


<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/danjm-dev/djm-core-services">
    <img src="https://i.imgur.com/dmkwuPo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">DJM Core Services</h3>

  <p align="center">
    Core services package for Unity Projects.
    <br />
    <a href="https://github.com/danjm-dev/djm-core-services"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/danjm-dev/djm-core-services/issues">Report Bug</a>
    ·
    <a href="https://github.com/danjm-dev/djm-core-services/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
         <li><a href="#dependencies">Dependencies</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

DJM Core Services is a Unity package for core game services that can be used across a range of projects.

The current implementation is a work in progress, but includes the following services.

- Application Controller
- Audio Source Pool      
- Debug Logger           
- Loading Screen Service
- Music Service          
- Persistant Event Manager
- Scene Loader           
- Scoped Event Manager   
- Transient Sound Service

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

The DJM Core Services package can be added to your project with [Unity's Package Manager](https://docs.unity3d.com/Manual/Packages.html). See installation instructions below.


### Required Dependencies

The following dependencies must be manually installed prior to DJM Core Services. 

#### [DOTween](http://dotween.demigiant.com/index.php)
Third party asset available on the Unity asset store. Either the [Pro](https://assetstore.unity.com/packages/tools/visual-scripting/dotween-pro-32416) or [Free](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676) version will work. 
1. Install and configure DOTween according to its [installation instructions](http://dotween.demigiant.com/getstarted.php). Ensure the UI module is included.
2. Once installed, press 'add ASMDEF' from the DoTween utility panel to create an assembly that djm-core-services can reference.

#### [DJM Event Manager](https://github.com/danjm-dev/djm-event-manager)
Another git based UPM package. Find [installation instructions](https://github.com/danjm-dev/djm-event-manager) on its readme.

#### [DJM Dependency Injection](https://github.com/danjm-dev/djm-dependency-injection)
Another git based UPM package. Find [installation instructions](https://github.com/danjm-dev/djm-dependency-injection) on its readme.


### Installation
1. In your unity project, open [Unity's Package Manager](https://docs.unity3d.com/Manual/Packages.html) from Window > Package Manager.
2. Open the add package menu (+) and select "Add package from git URL...".

   ![add package from github](https://i.imgur.com/a9yYzDh.png)
3. Enter the DJM Core Services repo URL : https://github.com/danjm-dev/djm-core-services.git, and press Add.

For more information about installing Unity packages from git, see [Unity Documentation](https://docs.unity3d.com/2022.3/Documentation/Manual/upm-ui-giturl.html).

<p align="right">(<a href="#readme-top">back to top</a>)</p>


## Usage

There are two primary ways to use services from this package.

### 1. Resolve Services with Service Locator

Services can be resolved via a static service locator class. 
The service locator is initialized before the first scene is loaded, so is safe to reference during Unity's awake phase.

```csharp
var logger = DJMServiceLocator.Resolve<IDebugLogger>();
```

Services are bound to the service locator and lazily instantiated when required.
Services scoped to "single" only get instantiated the first time they are resolved, with the same instance returned for subsequent resolutions.
Services scoped as "transient" get instantiated each time the service is resolved.

The service locator uses reflection to instantiate services, which can impact performance.
Although this impact should be minor, I recommend caching instances of services during the awake or start phase to minimise any impact.

```csharp
public class MainMenuManager : MonoBehaviour
{
    private IDebugLogger _debugLogger;
    private ISceneLoader _sceneLoader;

    private void Awake()
    {
        _debugLogger = DJMServiceLocator.Resolve<IDebugLogger>();
        _sceneLoader = DJMServiceLocator.Resolve<ISceneLoader>();
    }

    public void LoadLevelOneScene()
    {
        _debugLogger.LogInfo("Loading Level One", nameof(MainMenuManager));
        _sceneLoader.LoadScene("LevelOne");
    }
}
```

### 2. Manually Initialize Services

If you dont want to utilize the service locator and associated architecture, you can initialize instances of most services yourself.
The concrete classes implemented for each service are listed below. 

There is currently no way to disable the service locator without making code changes. However, as the services are loaded lazily, 
leaving it on should only result in a negligible performance cost when the first scene is loaded. 

If you'd like to Disable the service locator completely, comment out the `RuntimeInitializeOnLoadMethod` attribute in [Bootstrapper.cs](./Runtime/ServiceLocator/Bootstrapper.cs).
If you do this, remember that any calls to `DJMServiceLocator` will result in an `InvalidOperationException`.


### Services

These are the current services implemented in DJM Core Services. XML Documentation is available on the interface of each service.

| Service                  | Interface                                                                | Implementation               | Service Locator Scope | Description                                                                                                                                                                                                                                                                                                                                                                  |
|--------------------------|--------------------------------------------------------------------------|------------------------------|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Application Controller   | [IApplicationController](Runtime/Interfaces/IApplicationController.cs)   | ApplicationControllerService | Single                | Controls the application's game state.                                                                                                                                                                                                                                                                                                                                       |
| Audio Source Pool        | [IAudioSourcePool](Runtime/Interfaces/IAudioSourcePool.cs)               | AudioSourcePoolService       | Single                | Manages a pool of audio source components. Used internally by music and transient sound services.                                                                                                                                                                                                                                                                            |
| Debug Logger             | [IDebugLogger](Runtime/Interfaces/IDebugLogger.cs)                       | DebugLoggerService           | Single                | Centralized logging for debug messages with different severity levels.                                                                                                                                                                                                                                                                                                       |
| Loading Screen Service   | [ILoadingScreenService](Runtime/Interfaces/ILoadingScreenService.cs)     | LoadingScreenService         | Single                | Controls loading screen components. Used internally by Scene Loader.                                                                                                                                                                                                                                                                                                         |
| Music Service            | [IMusicService](Runtime/Interfaces/IMusicService.cs)                     | MusicService                 | Single                | Provides audio playback functionality for music.                                                                                                                                                                                                                                                                                                                             |
| Persistant Event Manager | [IPersistantEventManager](Runtime/Interfaces/IPersistantEventManager.cs) |                              | Single                | Persistent event manager used by internal services. Subscribers persist through scene loading. Should not be used for non-persistent event listeners (use Scoped Event Manager instead). If it is, ensure all non-persistent listeners are unsubscribed before a new scene is loaded. If not using service locator, use EventManager from djm-event-manager package instead. |
| Scene Loader             | [ISceneLoader](Runtime/Interfaces/ISceneLoader.cs)                       | SceneLoaderService           | Single                | Handles scene loading operations.                                                                                                                                                                                                                                                                                                                                            |
| Scoped Event Manager     | [IScopedEventManager](Runtime/Interfaces/IScopedEventManager.cs)         |                              | Single                | Event manager scoped to the lifecycle of a scene. All listeners cleared when new scene loaded. If not using service locator, use EventManager from djm-event-manager package instead.                                                                                                                                                                                        |
| Transient Sound Service  | [ITransientSoundService](Runtime/Interfaces/ITransientSoundService.cs)   | TransientSoundService        | Single                | Provides audio playback functionality for transient sounds.                                                                                                                                                                                                                                                                                                                  |




<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Dan - danjm.dev@gmail.com

Project Link: [https://github.com/danjm-dev/djm-core-services](https://github.com/danjm-dev/djm-core-services)

<p align="right">(<a href="#readme-top">back to top</a>)</p>




<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/danieljohnmiller/event-manager.svg?style=for-the-badge
[contributors-url]: https://github.com/danieljohnmiller/event-manager/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/danieljohnmiller/event-manager.svg?style=for-the-badge
[forks-url]: https://github.com/danieljohnmiller/event-manager/network/members
[stars-shield]: https://img.shields.io/github/stars/danieljohnmiller/event-manager.svg?style=for-the-badge
[stars-url]: https://github.com/danieljohnmiller/event-manager/stargazers
[issues-shield]: https://img.shields.io/github/issues/danieljohnmiller/event-manager.svg?style=for-the-badge
[issues-url]: https://github.com/danieljohnmiller/event-manager/issues
[license-shield]: https://img.shields.io/github/license/danieljohnmiller/event-manager.svg?style=for-the-badge
[license-url]: https://github.com/danieljohnmiller/event-manager/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/daniel-miller-690978170/
[product-screenshot]: images/screenshot.png
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 
