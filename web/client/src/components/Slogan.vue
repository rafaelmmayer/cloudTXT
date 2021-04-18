<template>
    <div id="slogan" class="text-center">
      <h1>{{titulo}}<span class="fa fa-check"></span></h1>
      <br/>
      <h6 class="text-secondary">{{info}}</h6>
    </div>  
</template>

<script>
    export default {
        props: {
            titulo: String,
            info: String
        },
    }
</script>

<style>
#slogan{
  margin-top: 30px;
  margin-bottom: 30px;
}
</style>