import * as React from "react";
import Dropdown, { Option } from "react-dropdown";
import "react-dropdown/style.css";
import { Applicant } from "../../ViewModels/Applicant";
import "./ApplyComponent.css";

const options: Option[] = [
  { value: "1", label: "Option 1" },
  { value: "2", label: "Option 2" },
  { value: "3", label: "Option 3" }
];

class ApplyComponent extends React.Component<any, any> {
  constructor(props: any) {
    super(props);
    this.state = {
      applicant: new Applicant(0, "", "", ""),
      selectedInternshipOption: options[0]
    };

    this.handleInternshipChange = this.handleInternshipChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  public render() {
    return (
      <div>
        <p>Apply to an internship</p>
        <form>
          <input
            type="text"
            placeholder="First Name"
            onChange={e => {
              const currentApplicant = this.state.applicant;
              currentApplicant.FirstName = e.target.value;
              this.setState({ applicant: currentApplicant });
            }}
          />
          <input
            type="text"
            placeholder="Last Name"
            onChange={e => {
              const currentApplicant = this.state.applicant;
              currentApplicant.LastName = e.target.value;
              this.setState({ applicant: currentApplicant });
            }}
          />
          <input
            type="text"
            placeholder="Phone Number"
            onChange={e => {
              const currentApplicant = this.state.applicant;
              currentApplicant.PhoneNumber = e.target.value;
              this.setState({ applicant: currentApplicant });
            }}
          />
          <Dropdown
            options={options}
            onChange={this.handleInternshipChange}
            placeholder="Select an internship..."
            value={this.state.selectedInternshipOption}
          />
          <button type="submit" onClick={this.handleSubmit}>
            Apply
          </button>
        </form>
        <p>{this.state.selectedInternshipOption.label}</p>
        <p>{this.state.selectedInternshipOption.value}</p>
      </div>
    );
  }

  private handleInternshipChange(option: Option) {
    this.setState({ selectedInternshipOption: option });
  }

  private handleSubmit(e: any): void {
    e.preventDefault();
    alert(this.state.applicant.FirstName);
    alert(this.state.selectedInternshipOption.value);
  }
}

export default ApplyComponent;
