import axios from "axios";
import React, { useEffect, useState } from "react";
import { Color, CreateProductRequest, Price, Product } from "../models/api.models";
import { ColorElement } from "./ProductPage";
import { useParams } from "react-router-dom";

export const ProductEdit = () => {
    const params = useParams();

    const [colors, setColors] = useState<Color[]>([]);
    const [product, setProduct] = useState<Product>(new Product());
    const [prices, setPrices] = useState<Price[]>([]);
    const [newPrice, setNewPrice] = useState({
        price: 0,
        statusId: 1,
        isDefaultPrice: false,
        colorId: 1,
        productId: 1,
    });

    const [form, setForm] = useState({
        name: "",
        description: "",
        statusId: 1,
    });

    useEffect(() => {
        console.log(params.id);

        axios.get("/api/v1/color").then((response) => {
            setColors(response.data.payload);
        });
        axios.get(`/api/v1/product/${params.id}`).then((response) => {
            setProduct(response.data.payload);
            setPrices(response.data.payload.prices);

            setForm({
                name: response.data.payload.name,
                description: response.data.payload.description,
                statusId: response.data.payload.statusId,
            });
        });
    }, []);

    const handleChange = (event) => {
        setForm({
            ...form,
            [event.target.name]: event.target.value,
        });
    };

    const handleChangeNewPrice = (event) => {
        setNewPrice({
            ...newPrice,
            [event.target.name]: event.target.value,
        });
    };

    const handleUpdateSubmit = (event) => {
        event.preventDefault();

        const request = {
            ...form,
        };
        axios.put(`/api/v1/product/${params.id}`, request).then((response) => {
            window.alert("Producto actualizado");
        });
    };

    const removePrice = (id) => {
        const result = window.confirm("¿Seguro que deseas eliminar este precio?");
        if (result) {
            axios.delete(`/api/v1/productprices/${id}`).then((response) => {
                axios.get(`/api/v1/product/${params.id}`).then((response) => {
                    setProduct(response.data.payload);
                    setPrices(response.data.payload.prices);
                });
            });
        }
    };

    const handleAddNewPrice = () => {
        const request = {
            ...newPrice,
            productId: params.id,
        };
        axios.post(`/api/v1/productprices`, request).then((_) => {
            axios.get(`/api/v1/product/${params.id}`).then((response) => {
                setProduct(response.data.payload);
                setPrices(response.data.payload.prices);
            });
        });
    };

    return (
        <div>
            <h1>Actualizar producto</h1>
            <hr />

            <div className="row">
                <div className="col-8">
                    <h4>Producto</h4>
                    <hr />
                    <form>
                        <div className="form-group">
                            <label htmlFor="">Nombre</label>
                            <input className="form-control" type="text" name="name" value={form.name} onChange={handleChange} />
                        </div>
                        <div className="form-group">
                            <label htmlFor="">Descripcion</label>
                            <textarea
                                className="form-control"
                                name="description"
                                value={form.description}
                                onChange={handleChange}
                            ></textarea>
                        </div>
                        <div className="form-group">
                            <label htmlFor="">Estatus</label>
                            <select className="form-control" name="statusId" value={form.statusId} onChange={handleChange}>
                                <option value="1" selected>
                                    Activo
                                </option>
                                <option value="2">Cancelado</option>
                            </select>
                        </div>
                        <div className="form-gruoup">
                            <button onClick={handleUpdateSubmit} className="btn btn-primary" type="submit">
                                Actualizar
                            </button>
                        </div>
                    </form>
                </div>
            </div>

            <h3 className="mt-5">Precios</h3>
            <hr />
            {product && (
                <div>
                    <div className="form-row">
                        <div className="form-group col-2">
                            <input
                                type="number"
                                className="form-control"
                                name="price"
                                value={newPrice.price}
                                onChange={handleChangeNewPrice}
                                placeholder="Precio"
                            />
                        </div>
                        <div className="form-group col-3">
                            <select
                                name="colorId"
                                className="form-control"
                                onChange={handleChangeNewPrice}
                                value={newPrice.colorId}
                            >
                                {colors.map((color) => (
                                    <option key={color.id} value={color.id} style={{ color: `#${color.value}` }}>
                                        {color.name}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <div className="form-group col-4">
                            <input
                                id="isDefaultPrice"
                                type="checkbox"
                                name="isDefaultPrice"
                                checked={newPrice.isDefaultPrice}
                                onChange={handleChangeNewPrice}
                                placeholder="Precio"
                            />
                            <label htmlFor="isDefaultPrice">Establecer como precio por defecto</label>
                        </div>
                        <div className="form-group col-3">
                            <button className="btn btn-primary" onClick={handleAddNewPrice}>
                                Agregar
                            </button>
                        </div>
                    </div>

                    <table className="table ">
                        <thead>
                            <tr>
                                <th>Precio</th>
                                <th>Color</th>
                                <th>Por Defecto</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            {prices?.map((price) => (
                                <tr key={price.id}>
                                    <td>
                                        <input type="text" className="form-control" value={price.price} />
                                    </td>
                                    <td>
                                        <select className="form-control" value={price.colorId}>
                                            {colors.map((color) => (
                                                <option key={color.id} value={color.id} style={{ color: `#${color.value}` }}>
                                                    {color.name}
                                                </option>
                                            ))}
                                        </select>
                                    </td>
                                    <td>{price.isDefaultPrice ? "SI" : "NO"}</td>
                                    <td>
                                        <button className="btn btn-danger" onClick={() => removePrice(price.id)}>
                                            Eliminar
                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            )}
        </div>
    );
};
