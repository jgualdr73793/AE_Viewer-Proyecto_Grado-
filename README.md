<div align="center">

# 🛡️ Virtual Twin for Military Training
**AI & Photogrammetry Pipeline**

*Proyecto desarrollado para el Ejército Nacional de Colombia*

<img src="(![Interfaz APP]https://github.com/user-attachments/assets/4b7eefae-47a6-4c3e-8192-96087975e564)" alt="Interfaz APP" width="70%">

[![Python](https://img.shields.io/badge/Python-3776AB?style=for-the-badge&logo=python&logoColor=white)]()
[![TensorFlow](https://img.shields.io/badge/TensorFlow-FF6F00?style=for-the-badge&logo=tensorflow&logoColor=white)]()
[![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)]()
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)]()
[![Blender](https://img.shields.io/badge/Blender-F5792A?style=for-the-badge&logo=blender&logoColor=white)]()

</div>

---

> **Resumen Ejecutivo:** Aplicación móvil interactiva para el entrenamiento táctico en identificación de Artefactos Explosivos (AE) y Artefactos Explosivos Improvisados (AEI). Mediante un pipeline que integra segmentación semántica (IA) y fotogrametría, el sistema permite reducir riesgos operativos y estandarizar los protocolos de reconocimiento mediante *Gemelos Virtuales* de alta fidelidad.

---

## ⚙️ Arquitectura y Pipeline Técnico (End-to-End)

El sistema fue construido mediante un pipeline de 4 fases que conecta Inteligencia Artificial con renderizado 3D en tiempo real:

### 1. Recolección de Datos y Segmentación (U-Net)
* Se construyó un dataset de **+500 imágenes** tomadas en campo.
* Se entrenó un modelo de segmentación semántica basado en la arquitectura **U-Net** para crear máscaras binarias.
* **🎯 Resultado:** Exactitud del **96.2%**, aislando eficientemente el artefacto de fondos complejos.

<div align="center">
  <img src="URL_FOTO_AQUI_Precision_Modelo_Reconocimiento" alt="Precisión Modelo U-Net" width="70%">
  <p><em>Máscaras binarias generadas y superpuestas sobre los objetos de entrenamiento.</em></p>
</div>

### 2. Reconstrucción 3D (Fotogrametría Computacional)
* Utilizando las imágenes pre-segmentadas, se usó **Meshroom** para generar nubes de puntos densas.
* Se aprovechó la aceleración por hardware (**CUDA**) para capturar detalles anatómicos críticos.
* **🎯 Resultado:** Reconstrucciones con un error dimensional medio de apenas **0.89 mm**.

<div align="center">
  <img src="URL_FOTO_AQUI_Fotogrametria_Meshroom" alt="Fotogrametría Meshroom" width="70%">
  <p><em>Nube de puntos densa y reconstrucción inicial utilizando Meshroom.</em></p>
</div>

### 3. Optimización Topológica
* Las mallas 3D crudas fueron importadas a **Blender** para un proceso exhaustivo de limpieza (*Retopology*).
* **🎯 Resultado:** Reducción del **60% en el conteo de polígonos**, optimizando el modelo para móviles sin sacrificar detalles.

<div align="center">
  <img src="URL_FOTO_AQUI_Limpieza_de_poligonos" alt="Limpieza de Polígonos en Blender" width="70%">
  <p><em>Proceso de optimización de polígonos y limpieza de malla en Blender.</em></p>
</div>

### 4. Despliegue Interactivo (Unity)
* Los modelos optimizados (`.fbx`) se integraron en el motor gráfico de **Unity**.
* Se desarrollaron scripts en **C#** para controlar la interacción táctil (rotación, zoom, selección).
* **🎯 Resultado:** La aplicación corre de manera estable a **58 FPS** en dispositivos Android.

<div align="center">
  <img src="URL_FOTO_AQUI_Compilacion_Unity" alt="Compilación en Unity" width="70%">
  <p><em>Interfaz y jerarquía de objetos configurados desde el editor de Unity.</em></p>
  <br>
  <img src="URL_FOTO_AQUI_Panel_Descriptivo" alt="Panel Descriptivo App" width="70%">
  <p><em>Cuadro descriptivo interactivo para la visualización detallada de componentes.</em></p>
</div>

---

## 💻 Instalación y Configuración Local

### 🛠️ Prerrequisitos
* **Python 3.8** * **Git** y **Visual Studio Code** * *Opcional pero recomendado:* **CUDA 11.1.1** y **cuDNN 9.7.8** (Para aceleración GPU de NVIDIA).

### 🚀 Despliegue del Entorno (Windows)

Abre una terminal (`Git Bash` o la integrada en `VS Code`) y ejecuta:

```powershell
# 1. Clonar el repositorio
git clone <https://github.com/jgualdr73793/AE_Viewer-Proyecto_Grado->

# 2. Instalar herramienta de entornos virtuales
pip install virtualenv

# 3. Crear y aislar el entorno (Python 3.8)
virtualenv -p python3.8 env
cd env

# 4. Dar permisos y activar (PowerShell)
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process
./Scripts/activate

# 5. Instalar dependencias
pip install -r "../Ruta_archivo_requerimientos.txt"
```

## 💡 Recomendaciones de Hardware y Captura

* **Captura Óptima:** Utilizar la aplicación **Iriun Webcam** con la cámara de un smartphone de alta resolución.
* **Encuadre:** Mantener el objeto de interés estrictamente en el centro del encuadre para una correcta toma de muestras de color.
* **Optimización:** Si el almacenamiento es un problema, reducir el número de fotografías. El reconocimiento será exitoso siempre que se cubran los diferentes ángulos de la geometría del objeto.

---

## 📚 Referencias y Créditos Académicos

Este desarrollo se fundamenta tecnológica y teóricamente en las siguientes herramientas, marcos y fuentes bibliográficas:

* **Ejército Nacional de Colombia:** Dirección de Ingenieros Militares. *Acciones preventivas contra artefactos explosivos*.
* **AliceVision & Meshroom:** *Photogrammetric Computer Vision Framework* y software de fotogrametría.
* **Blender Foundation:** Suite de creación 3D *open source*.
* **TensorFlow:** Framework principal para Machine Learning.
* **OpenCV:** Biblioteca de visión artificial en Python.
* **DataScientest:** Teoría y arquitectura de modelos U-Net.
* **IBM & Encord:** Marcos teóricos para la segmentación de imágenes y visión artificial.
* **Pix4D:** Conceptos de fotogrametría aplicada.
* **Dassault Systèmes:** Conceptualización de *Virtual Twin Experience*.
* **Unity Technologies:** Guías de desarrollo para creación de interfaces de usuario (UI) en aplicaciones interactivas.
* **Aprende Machine Learning:** Detección de objetos con Python y redes neuronales.

---

> ⚠️ **Nota de Confidencialidad y Seguridad Institucional:** Este repositorio contiene una versión pública y segura orientada exclusivamente a demostrar la arquitectura de software, el pipeline de Inteligencia Artificial y la integración en Unity. Por normativas de seguridad del Ejército Nacional de Colombia, en este repositorio **NO se incluye ningún tipo de información táctica sensible, fórmulas químicas, componentes de fabricación real, ni modelos 3D con especificaciones exactas** de uso privativo. Es un proyecto con fines estrictamente tecnológicos y de exhibición de portafolio.
