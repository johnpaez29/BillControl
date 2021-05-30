import React from 'react';

class List extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            bills: []
        }
    }

    componentDidMount() {
        fetch('https://localhost:44395/Bill')
        .then(res => res.json())
        .then(result => {
            this.setState({
                bills: result
            })
        });
    }

    render() {
        return (
            <div>
                {this.state.bills.map((x, i) => <div key={i}>{x.name}</div>)}
            </div>
        );
    }
}

export default List;