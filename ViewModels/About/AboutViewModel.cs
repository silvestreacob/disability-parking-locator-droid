using dpark.ViewModels.Base;

namespace dpark.ViewModels.About
{
    public class AboutViewModel : BaseViewModel
    {
        public string Overview { get; private set; }
        public string Notes { get; private set; }
        public string ListHeading { get; private set; }

        public AboutViewModel()
        {
            Overview =
                "This project is supported by Disability and Communication Access Board, Department of " +
            "Health, State of Hawai'i.";

            Notes =
            "The information is provided by the community. The data is not real-time. Photos indicate a " +
            "designated spot, but do not guarantee standards compliance. Feel free to flag any spots " +
            "that you find odd.";

            ListHeading =
                "The app is built with Xamarin Platform and Xamarin.Forms, and takes advantage of " +
                "several other supporting technologies. Interested in collaborating? " +
                "Please send us a note.";
        }
    }
}
