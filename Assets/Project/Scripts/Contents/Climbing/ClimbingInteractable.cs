using UnityEngine.XR.Interaction.Toolkit;

namespace ArchiEugene.Climbing
{
    public class ClimbingInteractable : XRBaseInteractable
    {
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            Climber.ClimbingHand = args.interactorObject.transform.GetComponent<XRController>();
        }
        
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            if(Climber.ClimbingHand && Climber.ClimbingHand.name == args.interactorObject.transform.name)
                Climber.ClimbingHand = null;
        }
    }
}

