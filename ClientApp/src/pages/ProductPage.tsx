import axios, { AxiosResponse } from "axios";
import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import Pagination from "react-responsive-pagination";
import {
    Card,
    CardImg,
    CardText,
    CardBody,
    CardTitle,
    CardSubtitle,
    Button,
    CardDeck,
    InputGroup,
    InputGroupAddon,
    Input,
    Col,
    InputGroupButtonDropdown,
    DropdownItem,
    DropdownMenu,
    DropdownToggle,
} from "reactstrap";
import { APIResponse, Price, Product } from "../models/api.models";

export const ProductPage = () => {
    const history = useHistory();
    const [products, setProducts] = useState<Product[]>();
    const [pagination, setPagination] = useState<APIResponse<Product>>();

    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const [search, setSearch] = useState({
        value: "",
    });

    useEffect(() => {
        axios
            .get<APIResponse<Product>>("/api/v1/product", {
                params: {
                    pageSize: pageSize,
                    page: currentPage,
                    name: search.value,
                },
            })
            .then((response) => {
                setPagination(response.data);
                setProducts(response.data.payload);
            });
    }, [currentPage]);

    const handleChange = (event) => {
        setSearch({ value: event.target.value });
    };

    const handleClick = (event) => {
        event.preventDefault();

        axios
            .get<APIResponse<Product>>("/api/v1/product", {
                params: {
                    pageSize: pageSize,
                    page: currentPage,
                    name: search.value,
                },
            })
            .then((response) => {
                setPagination(response.data);
                setProducts(response.data.payload);
            });
    };

    return (
        <>
            <InputGroup>
                <Input placeholder="Buscar producto por nombre" value={search.value} onChange={handleChange} />
                <InputGroupAddon addonType="prepend">
                    <Button onClick={handleClick}>Buscar</Button>
                </InputGroupAddon>
            </InputGroup>
            <hr />
            {!pagination && <h3>Cargando...</h3>}
            {pagination && (
                <div>
                    <h4>{pagination?.pagination.total} productos encontrados</h4>
                    <hr />
                    <div className="row p-3">
                        {products?.map((product) => (
                            <Col lg={4} key={product.id} className="mt-2">
                                <Card>
                                    <CardBody>
                                        <CardTitle tag="h5">{product.name}</CardTitle>
                                        <CardSubtitle tag="h6" className="mb-2 text-muted">
                                            Descripción
                                        </CardSubtitle>
                                        <CardText>{product.description}</CardText>
                                        <hr />

                                        <CardSubtitle tag="h6" className="mb-2 text-muted">
                                            Precio: ${product.prices.find((e) => e.isDefaultPrice).price}
                                            <ColorElement colorValue={product.prices.find((e) => e.isDefaultPrice).colorValue} />
                                        </CardSubtitle>
                                        <hr />
                                        <CardSubtitle tag="h6" className="mb-2 text-muted">
                                            Disponible en:
                                            <br />
                                            {product.prices.map((price) => (
                                                <ColorElement key={price.id} colorValue={price.colorValue} />
                                            ))}
                                        </CardSubtitle>

                                        <Button
                                            onClick={() => {
                                                history.push(`products/${product.id}`);
                                            }}
                                        >
                                            Ver
                                        </Button>
                                    </CardBody>
                                </Card>
                            </Col>
                        ))}
                    </div>
                    <Pagination
                        current={currentPage}
                        total={Math.round(pagination?.pagination.total / pageSize)}
                        onPageChange={setCurrentPage}
                    />
                </div>
            )}
        </>
    );
};

export function ColorElement({ colorValue, handleClick }: any) {
    return (
        <button
            className="btn m-1"
            style={{
                backgroundColor: `#${colorValue}`,
                width: "10px",
                height: "10px",
                padding: "10px",
                borderRadius: "100px",
            }}
            onClick={handleClick}
        ></button>
    );
}
