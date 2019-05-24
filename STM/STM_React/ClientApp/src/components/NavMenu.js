import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            <header>
                <a href="#" className="logo">STM</a>
                <ul className="stm-menu">
                    <li><a href="#">Рабочий стол</a></li>
                    <li><a href="/Projects">Проекты</a></li>
                    <li><a href="/Boards">Доски</a></li>
                    <li><a href="/Tasks">Задачи</a></li>
                    <li>
                        <a href="/Setup">Setup</a>
                        <ul className="stm-submenu">
                            <li><a href="/Setup/Status"></a></li>
                            <li><a href="/Setup/Workflow"></a></li>
                            <li><a href="/Setup/Type"></a></li>
                        </ul>
                    </li>
                </ul>
                <div className="search"><input placeholder="search" className="stm-text" type="text" name="search" /></div>
            </header>
        );
    }
}
