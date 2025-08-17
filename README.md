# Light Probe Populator
## Automatic Unity Light Probe Group Generation

While I've found a few different automated light probe solutions for Unity, I couldn't easily find a free, easily accessible one - why not take a few minutes off at work and hash one out?

![header](images/header.png)

#### Why do I need light probes?
 As the Unity documentation puts it:
> Light Probes provide a way to capture and use information about light that is passing through the empty space in your scene.
>Light probes store “baked” information about lighting in your scene. The difference is that while lightmaps store lighting information about light hitting the surfaces in your scene, light probes store information about light passing through empty space in your scene.

If you're more familiar with Unreal Engine, this package is roughly what the Lightmass Importance Volume does for you, albeit in a slightly less optimized fashion.

![packman](images/packman.png)

### Install process
This package can be installed using the Unity Package Manager.

1.  Open the Package Manager in `Window > Package Manager`.
2.  Click the `+` button in the top left corner and select `Add package from git URL...`.
3.  Enter the git URL of this repository and click `Add`.

![footer](images/options.png)

### Usage
Just select one of the four options in the *Tools* menu item.
 - *Low Resolution* is best suited for stylized or mobile games
 - *Medium Resolution* is good for PC or Console
 - *High Resolution* is good if you're doing realtime rendering.
 - *Very High Resolution* is kind of overkill - use if you're trying to compete with the Unity demo team.

![footer](images/footer.png)

Created at [Imaginary Spaces](https://imaginary-spaces.com/) in the timespan of a coffee break.