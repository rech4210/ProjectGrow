
public interface I_Interaction
{
    ItemCtrl GrabItemCtrl => null;
    void initiallize();
    void InteractionEnter();
    void InteractionStay();
    void InteractionExit();
    void interactionGrap();
    void interactionThrow();

    void interactionGrabOff();
}
