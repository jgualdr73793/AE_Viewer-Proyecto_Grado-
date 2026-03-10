# 🛡️ Virtual Twin for Military Training: AI & Photogrammetry Pipeline

[cite_start]**Proyecto desarrollado para el Ejército Nacional de Colombia** [cite: 55, 87]

![Aplicación Funcional](URL_FOTO_APP_FUNCIONAL_AQUI)
[cite_start]*Vista de la aplicación móvil funcional en dispositivo Android con interfaz interactiva.* [cite: 148, 149, 150]

## 📖 Descripción del Proyecto
[cite_start]Este proyecto fue desarrollado como práctica profesional para crear un sistema tecnológico innovador orientado al entrenamiento en identificación de Artefactos Explosivos (AE) y Artefactos Explosivos Improvisados (AEI)[cite: 87]. 

[cite_start]A través de la virtualización y la creación de un "Gemelo Virtual", esta aplicación móvil permite reducir los riesgos en el entrenamiento básico, optimiza los procesos de reconocimiento logístico y permite a los usuarios interactuar de forma segura con modelos 3D altamente precisos[cite: 88, 89, 90, 106].

## 🛠️ Tecnologías y Herramientas
* [cite_start]**Inteligencia Artificial & Visión Computacional:** Python, TensorFlow, Keras, OpenCV, U-Net[cite: 92, 118, 119, 124, 128].
* [cite_start]**Fotogrametría & Modelado 3D:** Meshroom (AliceVision), Blender[cite: 126, 127].
* [cite_start]**Desarrollo de Software / App Móvil:** Unity 3D, C#, Android[cite: 98].

## ⚙️ Arquitectura y Pipeline Técnico (End-to-End)
El sistema fue construido mediante un pipeline de 4 fases que conecta IA con renderizado 3D en tiempo real:

### 1. Recolección de Datos y Segmentación (U-Net)
* [cite_start]Se construyó un dataset de más de 500 imágenes tomadas en campo[cite: 115, 160].
* [cite_start]Se entrenó un modelo de segmentación semántica basado en la arquitectura **U-Net** para crear máscaras binarias[cite: 92, 97].
* [cite_start]**Resultado:** El modelo alcanzó una exactitud del **96.2%**, aislando eficientemente el artefacto de fondos complejos e irregulares[cite: 134].

### 2. Reconstrucción 3D (Fotogrametría Computacional)
* [cite_start]Utilizando las imágenes procesadas y pre-segmentadas, se utilizó **Meshroom** para generar nubes de puntos densas[cite: 136, 137].
* [cite_start]Aprovechando la aceleración por hardware (CUDA), el sistema logró capturar detalles anatómicos críticos como mecanismos de activación y marcas de fabricación[cite: 137].
* [cite_start]**Resultado:** Reconstrucciones con un error dimensional medio de apenas **0.89 mm**, superando la fidelidad de algunos escáneres cilíndricos tradicionales[cite: 137, 154].

![Reconstrucción en Meshroom](URL_FOTO_MESHROOM_AQUI)
[cite_start]*Nube de puntos densa y reconstrucción inicial del AEI utilizando Meshroom.* [cite: 138, 139]

### 3. Optimización Topológica
* [cite_start]Las mallas 3D crudas fueron importadas a **Blender** para un proceso exhaustivo de limpieza[cite: 115, 141].
* [cite_start]**Resultado:** Reducción del **60% en el conteo de polígonos** (Retopology), optimizando el modelo para dispositivos móviles sin sacrificar detalles visuales[cite: 141, 161].

![Limpieza en Blender](URL_FOTO_BLENDER_AQUI)
[cite_start]*Proceso de optimización de polígonos y limpieza de malla en Blender.* [cite: 142, 143]

### 4. Despliegue Interactivo (Unity)
* [cite_start]Los modelos optimizados (`.fbx`) se integraron en el motor gráfico de **Unity**[cite: 115].
* [cite_start]Se desarrollaron scripts en **C#** para controlar la interacción del usuario (rotación táctil, zoom, cuadros descriptivos, selección de objetos)[cite: 115].
* [cite_start]**Resultado:** La aplicación móvil corre de manera estable a **58 FPS** en dispositivos Android de gama media/alta, garantizando una experiencia fluida[cite: 145, 148].

![Modelos en Unity](URL_FOTO_UNITY_AQUI)
[cite_start]*Interfaz, jerarquía de objetos y modelos 3D configurados desde el editor de Unity.* [cite: 145, 146]

## 💻 Instalación y Configuración (Entorno Local)

### Prerrequisitos
* **Python 3.8** instalado desde la página oficial.
* **Git** y **Visual Studio Code** instalados.

### Clonar el Repositorio
Abre tu explorador de archivos, dirígete a la carpeta donde deseas guardar el proyecto, haz clic derecho y selecciona **Open Git Bash here**. Ejecuta:

```bash
git init
git clone <AQUÍ_EL_LINK_DE_TU_REPOSITORIO>
