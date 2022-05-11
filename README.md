#About this PUN2's intro tutorial project, made in Unity 2021.3.0f1

This project is the result of Elias Ximenes *one week* study, about *how to make multiplayers games with Unity*!

All content in this repository so far is based on [Photon PUN2's intro tutorial](https://doc.photonengine.com/en-us/pun/current/demos-and-tutorials/pun-basics-tutorial/intro)

## Some small changes were made
Such as: 
- Not using the *region* tags
- And checking if the avatar is "alive", in the *Update* method, of the class: *PlayerManager*
  - To avoid calling the PhotonNetwork.LeaveRoom() function when not connected

## Bugs
Somehow, as yet unidentified, the repositioning of the avatars stopped working; for cases where some user is removed from the scene, causing a change of scene for all the others.

## Extras
In addition, the [Git plugin for Unity ](https://unity.github.com/) was imported into this project, aiming at a better "native" control of its versioning.

## This version will soon be amended to:

- Fix the repositioning problem;
- Add some UML documentation on how PUN2 network events work;
- And possibly an intense refactoring will also be done
  - Aiming at the reuse of classes capable of replicating these functionalities for other projects

## About this version
If what you want is a finished project, very similar to the PUN2 tutorial, that works for Unity 2021.3.0f1, this is the right version of the repository to download!