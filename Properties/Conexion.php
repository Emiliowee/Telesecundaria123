<?php
$host = "localhost";
$usuario = "root"; // Cambia si tienes otra configuración
$contrasena = "";  // Si tienes contraseña, ponla aquí
$bd = "sistema_escolar";

$conexion = new sqlServer($host, $usuario, $contrasena, $bd);

if ($conexion->connect_error) {
    die("Error de conexión: " . $conexion->connect_error);
}
?>
