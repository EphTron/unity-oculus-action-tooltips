# unity-oculus-action-labels
Simple extensible solution to show action labels for an Oculus controller.


# Setup
Add the TooltipLeft prefab and the TooltipRight prefab underneath your Oculus Controller representation.
Add a TooltipHandler.cs component to your user and the TooltipMapper.cs component from the left and right controller to it.

Any interaction that can be triggered by the controller can set, show, hide and remove tooltips through the TooltipHandler.
An example can be found with the HandRay.cs components in the ExampleScene.
