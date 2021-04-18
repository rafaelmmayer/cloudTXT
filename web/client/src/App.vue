<template>
  <div>
    <Slogan titulo="Projeto Prática de Formação B" info="Dashboard criado para download de arquivos armazenados na nuvem (MongoDb)"/>    
    <ListFiles :arquivos="arquivos"/>
  </div>
</template>

<script>
import "bootstrap/dist/css/bootstrap.css"
import "font-awesome/css/font-awesome.css"
import ListFiles from './components/ListFiles'
import Slogan from './components/Slogan'


export default {
  components: {
    ListFiles,
    Slogan
  },
  data() {
    return {
      arquivos: [],
    }
  },
  created() {
    this.$http.get("https://projeto-pf.herokuapp.com/api")
      .then(res => res.json())
      .then(json => this.arquivos = json)
  },
}
</script>

<style>

</style>
