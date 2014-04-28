namespace PicSim.Components.Communication
{
    public interface ICommunication
    {
        bool Open();
        bool Close();
        bool Reset();

        uint ReadValue();
        bool WriteValue(uint _data);
    }
}
