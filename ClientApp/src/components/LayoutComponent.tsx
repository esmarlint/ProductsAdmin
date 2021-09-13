import React from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";

export const LayoutComponent = (props) => {
    return (
        <div>
            <NavMenu />
            <Container>{props.children}</Container>
        </div>
    );
};
