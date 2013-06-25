# How to contribute

We welcome your contributions. If you have a new meme to include, send it our way
via pull request (details below). Bug fixes, typos, and minor changes can probably
be sent over unsolicited, too. 

If you have some significant changes, though, you might want to reach out to us
first. Although we will most likely be very excited to merge your change, there's
a chance we'll have...artistic differences. We don't want you to waste your time on
something that we can't merge later!

## Getting Started

* Make sure you have a [GitHub account](https://github.com/signup/free)
* Open an [issue](https://github.com/worldwidewat/upboat.me/issues) to discuss your planned changes
* Fork the repository on GitHub and go to town

## Making Changes

* Be courteous with your commits and commit messages
* Try to follow the coding style of the neighborhood your changes are in
* Test your changes thoroughly

### Adding a new meme

* Add a blank template image to [`UpboatMe/Images/Originals`](https://github.com/worldwidewat/upboat.me/tree/master/UpboatMe/Images/Original)
    * We will use this to generate a smaller image in a minute
* Re-save your "raw" template as a relatively low quality png to [`UpboatMe/Images`](https://github.com/worldwidewat/upboat.me/tree/master/UpboatMe/Images) with the following settings (this often has a *dramatic* impact on file size):
    * Width <= 400px
    * Type: png
    * Bit depth: 8-bit (many apps, like [Paint.Net](http://www.getpaint.net/), let you set this when you save)
    * If you can't get the file size down, don't worry about it. Commit anyway (we'll fix it up!)
* Include both the original _and_ the smaller version with your commit (so we can rebuild the smaller one later if we need to)
* Update [`UpboatMe/App_Start/MemeConfig.cs`](https://github.com/worldwidewat/upboat.me/blob/master/UpboatMe/App_Start/MemeConfig.cs) 
  with the new meme's metadata (it's pretty obvious...)
    * Put your new meme in alphabetical order with the others (please!)
* If you didn't add the new image from within Visual Studio, be sure to include the new image in the project file. Do this by 
  clicking the "Show all files" button in Solution Explorer, then right click on the image and choose "Include in project"
* Test your new meme (don't forget to rebuild!)

## Submitting Changes

* Append your name directly to the [Contributor License Agreement](https://github.com/worldwidewat/upboat.me/blob/master/CONTRIBUTORS.md) 
  and commit it. Include this with your pull request. (We cannot accept pull requests unless you sign the CLA.) 
* Submit your changes (included your name on the CLA file) to us as a pull request

## Additional Resources
* [Michael](http://twitter.com/mharen) and [Mark](http://twitte.com/pwninstein) on Twitter
* [General GitHub documentation](http://help.github.com/)
* [GitHub pull request documentation](http://help.github.com/send-pull-requests/)

(This file was adapted from the [Puppet Labs contributing guide](https://github.com/puppetlabs/puppet/blob/master/CONTRIBUTING.md).)
