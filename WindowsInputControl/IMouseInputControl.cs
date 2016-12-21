namespace WindowsInputControl
{
    public interface IMouseInputControl
    {

        void ButtonClick(MouseButton button);
        void ButtonDoubleClick(MouseButton button);
        void ButtonDown(MouseButton button);
        void ButtonUp(MouseButton button);        


        void LeftButtonClick();
        void LeftButtonDoubleClick();
        void LeftButtonDown();
        void LeftButtonUp();

        void RightButtonClick();
        void RightButtonDoubleClick();
        void RightButtonDown();
        void RightButtonUp();

        void XButtonClick(int buttonId);
        void XButtonDoubleClick(int buttonId);
        void XButtonDown(int buttonId);
        void XButtonUp(int buttonId);


        void MoveMouseBy(int pixelDeltaX, int pixelDeltaY);
        void MoveMouseTo(double absoluteX, double absoluteY);
        void MoveMouseToPositionOnVirtualDesktop(double absoluteX, double absoluteY);


        void HorizontalScroll(int scrollAmountInClicks);
        void VerticalScroll(int scrollAmountInClicks);
    }
}