;(function() {
    "use strict";

    var TextInput = React.createClass({
        getInitialState: function() {
            return { value: "" };
        },
        resetValue: function() {
            this.setState(this.getInitialState());
        },
        handleChange: function(event) {
            this.setState({value: event.target.value});
        },
        render: function() {
            var id = _.uniqueId("TextInput");
            return (
                <div>
                    <label htmlFor={id}>{this.props.label}</label>
                    <input type="text" 
                           id={id} 
                           value={this.state.value} 
                           onChange={this.handleChange}
                    />
                </div>);
        }
    });

    var AddEmployeeForm = React.createClass({
        handleSubmit: function(event) {
            event.preventDefault();
            if (this.props.onEmployeeAdded) {
                this.props.onEmployeeAdded({
                    firstName: this.refs.firstName.state.value,
                    lastName: this.refs.lastName.state.value
                });

                this.refs.firstName.resetValue();
                this.refs.lastName.resetValue();
            }
        },
        render: function() {
            return (
                <form onSubmit={this.handleSubmit}>
                    <TextInput ref="firstName" label="First name:" />
                    <TextInput ref="lastName" label="Last name:" />
                    <input type="submit" value="Add Employee" />
                </form>);
        }
    });

    var EmployeeTable = React.createClass({
        render: function() {
            return (
                <table>
                    <thead>
                        <tr>
                            <th>First Name</th>
                            <th>Last Name</th>
                        </tr>
                    </thead>
                    <tbody>{
                        this.props.employees.map(function (employee) {
                            return (
                                <tr>
                                    <td>{employee.FirstName}</td>
                                    <td>{employee.LastName}</td>
                                </tr>);
                        })
                    }</tbody>
                </table>);
            return ;
        }
    });

    var EmployeeForm = React.createClass({

        componentDidMount: function() {
            var employeeHub = $.connection.employeeHub;
            var self = this;

            employeeHub.client.showEmployees = function (data) {
                var employees = $.parseJSON(data);
                self.setState({ employees: employees });
            };

            $.connection.hub.start().done(function () {
                employeeHub.server.getEmployees();
            });
        },

        getInitialState: function() {
            return { employees: [] };
        },

        handleEmployeeAdded: function(employee) {
            $.connection.employeeHub.server.addEmployee(
                employee.firstName, employee.lastName
            );
        },
        
        render: function() {
            return (
                <div>
                    <AddEmployeeForm onEmployeeAdded={this.handleEmployeeAdded} />
                    <EmployeeTable employees={this.state.employees} />
                </div>);
        }
    });

    var appContainer = document.getElementById('app-container');
    React.render(<EmployeeForm/>, appContainer);
})();