import axios from "axios";
import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { APIResponse, Product } from "../models/api.models";
import Pagination from "react-responsive-pagination";

export const ProductAdmin = () => {
    const history = useHistory();
    const [products, setProducts] = useState<Product[]>();
    const [pagination, setPagintion] = useState<APIResponse<Product>>();
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    useEffect(() => {
        axios
            .get<APIResponse<Product>>("/api/v1/product", {
                params: { pageSize: pageSize, page: currentPage },
            })
            .then((response) => {
                setPagintion(response.data);
                setProducts(response.data.payload);
            });
    }, [currentPage]);

    const handleClick = (event) => {
        event.preventDefault();

        axios.get<APIResponse<Product>>("/api/v1/product").then((response) => {
            setPagintion(response.data);
            setProducts(response.data.payload);
        });
    };

    const remove = (id) => {
        const result = window.confirm("¿Seguro que deseas eliminar este producto?");
        if (result) {
            axios.delete(`/api/v1/product/${id}`).then((_) => {
                axios.get<APIResponse<Product>>("/api/v1/product").then((response) => {
                    setPagintion(response.data);
                    setProducts(response.data.payload);
                });
            });
        }
    };

    return (
        <>
            <h1>Listado de productos</h1>
            <hr />
            <button
                onClick={() => {
                    history.push("/products/admin/create");
                }}
                className="btn btn-primary mb-2"
            >
                Crear producto
            </button>

            <table className="table table-striped ">
                <thead>
                    <tr>
                        {/* <th>Id</th> */}
                        <th>Nombre</th>
                        <th>Descripción</th>
                        <th>Estatus</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    {products?.map((product) => (
                        <tr>
                            {/* <td>{product.id.toString()}</td> */}
                            <td>{product.name}</td>
                            <td>{product.description.slice(1, 40)}</td>
                            <td>{product.status}</td>
                            <td>
                                <button
                                    className="btn btn-info mr-2"
                                    onClick={() => {
                                        history.push(`/products/admin/edit/${product.id}`);
                                    }}
                                >
                                    Editar
                                </button>
                                <button type="button" className="btn btn-danger mb" onClick={() => remove(product.id)}>
                                    Eliminar
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>

                {pagination && (
                    <Pagination
                        current={currentPage}
                        total={Math.round(pagination?.pagination.total / pageSize)}
                        onPageChange={setCurrentPage}
                    />
                )}
            </table>
        </>
    );
};
