<template>
    <div class="col-md">
      <h5>Conteúdo</h5>
      <div class="card">
        <div class="card-body contentCard">
          <span>{{arquivoSelecionado.conteudo}}</span>
        </div>
      </div>
    </div>
</template>

<script>
    export default {
        props: {
            arquivoSelecionado: Object
        },
    }
</script>

<style>
.contentCard{
  height: 500px;
}
</style>